using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Speed")]
    public float acceleration = 3f;
    public float walkStopRate = 0.01f;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float SuperRunSpeed = 12f;
    public float crouchSpeed = 2f;
    public float climbVelocity;
    public float fallingWallVelocity = 2f;
    public float runCountdown = 4f;

    [Header("Jump Impulses")]
    public float jumpImpulse = 10f;
    public float superJumpImpulse = 26f;
    public float wallJumpImpulseY = 6f;
    public float wallJumpImpulseX = 5f;

    [Header("Number Objects")]
    public int arrows;
    public int maxArrows;
    public int healers;
    public int maxHealers;
    public int healthRestore = 10;
    public int coins;

    Vector2 moveInput;
    TouchingDirections touchingdirections;
    Damageable damageable;

    Rigidbody2D rb;
    Animator animator;

    [Header("Booleans")]
    public bool _isFacingRight = true;
    public bool _hasSuperJump = true;
    public bool HasWallJump;
    public bool HasBow;
    public bool ReduceArrows;
    public bool HasHealers;
    public bool HasQuiver;
    public bool HasClimb;
    public bool CanClimb;

    private int FacingDirection = 1;
    public bool IsJumping;
    private bool _isMoving = false;
    private bool _isRangeAttacking = false;
    private bool IsCrouched;
    private bool ClimbButtonHeld;

    private float CurrentMoveSpeed { get
    
     {
        if (CanMove && !_isRangeAttacking)
        {
            if (IsMoving && !touchingdirections.IsOnWall)
            {
                if(IsRunning && !IsSuperRunning && !IsCrouched)
                {
                    return runSpeed;
                }
                else if (IsSuperRunning && IsRunning && !IsCrouched)
                {
                    return SuperRunSpeed;
                }
                else if (IsCrouched)
                {
                    return crouchSpeed;
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
            FacingDirection = -FacingDirection;
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

    public bool CanWallJump
    {
        get
        {
            if (HasWallJump)
            {
                return animator.GetBool(AnimationStrings.canWallJump);
            }
            else 
            {
                return false;
            }
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
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (acceleration * FacingDirection), -CurrentMoveSpeed, CurrentMoveSpeed), rb.velocity.y);
        }
        if(rb.velocity.y <= 0)
        {
            rb.gravityScale = 3.5f;
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

        if(touchingdirections.IsOnWall && !touchingdirections.IsGrounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(wallJumpImpulseX, -fallingWallVelocity);
        }

        if(!damageable.IsAlive)
        {
            damageable.Health = 0;
        }

        if(ClimbButtonHeld && CanClimb)
        {
            animator.SetBool(AnimationStrings.climb, true);
            rb.velocity = new Vector2(rb.velocity.x, climbVelocity);
        }

        if(!CanClimb)
        {
            ClimbButtonHeld = false;
            animator.SetBool(AnimationStrings.climb, false);
        }

        if(touchingdirections.IsGrounded || touchingdirections.IsOnWall)
        {
            IsJumping = false;
            rb.gravityScale = 2;
        }

        if (rb.velocity.y > 0)
        {
            IsJumping = true;
        }
        if(rb.velocity.y < -18)
        {
            rb.velocity = new Vector2(rb.velocity.x, -18);
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
        runCountdown = 4f;
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
        if(context.started && touchingdirections.IsGrounded && CanMove && !_hasSuperJump && !CanClimb)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }

        else if (context.canceled && IsJumping)
        {
            rb.gravityScale = 3.5f;
        }
        else if(context.started && touchingdirections.IsGrounded && CanMove && _hasSuperJump && !CanClimb)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, superJumpImpulse);
        }
        
        else if (context.started && CanWallJump && !CanClimb)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x * wallJumpImpulseX, wallJumpImpulseY);
        }

        else if (context.started && CanClimb)
        {
            ClimbButtonHeld = true;
        }

        if (context.canceled && ClimbButtonHeld)
        {
            ClimbButtonHeld = false;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }
        IsSuperRunning = false;

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
            IsSuperRunning = false;
        }
    }

    public void OnRangeAttack(InputAction.CallbackContext context)
    {
        if (context.started && arrows > 0 && HasBow)
        {
            animator.SetTrigger(AnimationStrings.rangeAttack);
            ReduceArrows = true;
        }
        IsSuperRunning = false;
        
        
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetBool(AnimationStrings.crouched, true);
            IsCrouched = true;
            
        }

        else if (context.canceled)
        {
            animator.SetBool(AnimationStrings.crouched, false);
            IsCrouched = false;
        }
        IsSuperRunning = false;
        
        
    }
}
