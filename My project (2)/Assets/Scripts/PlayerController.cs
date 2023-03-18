using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float SuperRunSpeed = 12f;
    public float runCountdown = 4f;
    public int arrows = 3;

    public int healers = 5;

    public int healthRestore = 10;

    Vector2 moveInput;
    TouchingDirections touchingdirections;
    Damageable damageable;

    Rigidbody2D rb;
    Animator animator;

    private bool _isMoving = false;
    private bool _isRangeAttacking = false;
    public bool _isFacingRight = true;
    public bool _hasSuperJump = true;
    public float jumpImpulse = 10f;
    public float superJumpImpulse = 26f;

    private float CurrentMoveSpeed { get
    
     {
        if (CanMove && !_isRangeAttacking)
        {
            if (IsMoving && !touchingdirections.IsOnWall)
            {
                if(IsRunning && !IsSuperRunning)
                {
                    return runSpeed;
                }
                else if (IsSuperRunning && IsRunning)
                {
                    return SuperRunSpeed;
                }
                else
                {
                    return walkSpeed;
                }
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }}


    public bool IsFacingRight{get {return _isFacingRight;  } private set {
        if(_isFacingRight != value && damageable.IsAlive)
        {
            transform.localScale *= new Vector2(-1, 1);
        }
        _isFacingRight = value;

    }}

    public bool IsMoving { get 
     {
        return _isMoving;
     }
      private set 
     {
        _isMoving = value;
        animator.SetBool(AnimationStrings.isMoving, value);
     }
    }


    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    private bool _isSuperRunning = false;

    public bool IsSuperRunning
    {
        get
        {
            return _isSuperRunning;
        }
        set
        {
            _isSuperRunning = value;
            animator.SetBool(AnimationStrings.isSuperRunning, value);
        }
    }

    public bool CanMove{get
    {
        return animator.GetBool(AnimationStrings.canMove);
    
    }  }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingdirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if(!damageable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }
        

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    private void Update()
    {
        if(IsRunning && IsMoving && runCountdown > 0)
        {
            runCountdown -= Time.deltaTime;
        }
        if (runCountdown < 0 && IsMoving)
            {
                IsSuperRunning = true;
                

            } else if(runCountdown > 0)

            { 
                walkSpeed = 5f;
            }
        if (touchingdirections.IsGrounded && ! IsMoving)
        {
            runCountdown = 4f;
            IsSuperRunning = false;
        }


        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            //face right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // face left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;

        } else if (context.canceled)
        {
            IsRunning = false;
            runCountdown = 4f;

        }
        

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingdirections.IsGrounded && CanMove && !_hasSuperJump || context.started && touchingdirections.IsOnWall && CanMove && !_hasSuperJump)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
        else if(context.started && touchingdirections.IsGrounded && CanMove && _hasSuperJump || context.started && touchingdirections.IsOnWall && CanMove && !_hasSuperJump)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, superJumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }

    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnHeal (InputAction.CallbackContext context)
    {
        if(context.started && damageable.IsAlive && damageable.Health < damageable.MaxHealth && touchingdirections.IsGrounded && healers > 0)
        {
            damageable.Health += healthRestore;
            healers -= 1;

            animator.SetTrigger(AnimationStrings.heal);
            if(damageable.Health > damageable.MaxHealth)
            {
                damageable.Health = damageable.MaxHealth;
            }

            CharacterEvents.characterHealed(gameObject, healthRestore);
        }
    }

    public void OnRangeAttack(InputAction.CallbackContext context)
    {
        if (context.started && arrows > 0)
        {
            animator.SetTrigger(AnimationStrings.rangeAttack);
            
            arrows -= 1;
        }
        
        
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        Debug.Log("YEAH");
        if (context.started)
        {
            animator.SetBool(AnimationStrings.crouched, true);
            
        }

        else if (context.canceled)
        {
            animator.SetBool(AnimationStrings.crouched, false);
        }
        
        
    }
}
