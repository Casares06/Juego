using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDataPersistence
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
    public float dashTimer = 1f;
    public float dashForce;
    public float numDash;
    public float numDashHave;
    public float dashRegenTimer = 0;

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
    HealthBar HP;

    Rigidbody2D rb;
    Animator animator;
    Transform transform;

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
    private bool HasWallJumped;
    private bool HasDashed;

    private int FacingDirection = 1;
    private bool IsJumping;
    private bool _isMoving = false;
    private bool _isRangeAttacking = false;
    private bool IsCrouched;
    private bool ClimbButtonHeld;
    private float wallJumpTimer = 0.1f;
    private bool CanWallJumpAgain;
    private bool IsSliding = false;
    public bool Interacted;
    private float slideTimer = 3f;

    public void LoadData(GameData data)
    {
        this.arrows = data.arrows;
        this.maxArrows = data.maxArrows;
        this.healers = data.healers;
        this.maxHealers = data.maxHealers;
        this.coins = data.coins;
        this._hasSuperJump = data._hasSuperJump;
        this.HasWallJump = data.HasWallJump;
        this.HasBow = data.HasBow;
        this.HasHealers = data.HasHealers;
        this.HasQuiver = data.HasQuiver;
        this.HasClimb = data.HasClimb;
    }

    public void SaveData (GameData data)
    {
        data.arrows = this.arrows;
        data.maxArrows = this.maxArrows;
        data.healers = this.healers;
        data.maxHealers = this.maxHealers;
        data.coins = this.coins;
        data._hasSuperJump = this._hasSuperJump;
        data.HasWallJump = this.HasWallJump;
        data.HasBow = this.HasBow;
        data.HasHealers = this.HasHealers;
        data.HasQuiver = this.HasQuiver;
        data.HasClimb = this.HasClimb;
    }

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
        transform = GetComponent<Transform>();
        HP = GameObject.Find("HealthBar").GetComponentInChildren<HealthBar>();
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
        if(HasWallJumped)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(wallJumpImpulseX * transform.localScale.x, wallJumpImpulseY);
        }
        if (HasDashed)
        {
            rb.velocity = new Vector2(rb.velocity.x * dashForce, 0);
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

        if(touchingdirections.IsGrounded)
        {
            IsJumping = false;
            HasWallJumped = false;
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
        if(wallJumpTimer > 0 && HasWallJumped)
        {
            wallJumpTimer -= Time.deltaTime;
        }
        else
        {
            HasWallJumped = false;
            wallJumpTimer = 0.1f;
        }

        if (!touchingdirections.IsOnWall && !touchingdirections.IsGrounded || touchingdirections.IsGrounded)
        {
            CanWallJumpAgain = true;
        }

        if(IsSuperRunning)
        {
            Shadows.me.ShadowsSkill();
        }

        if(HasDashed)
        {
            dashTimer -= Time.deltaTime;
            Shadows.me.ShadowsSkill();

            if(dashTimer <= 0)
            {
                animator.SetBool("Dash", false);
                numDash -= 1;
                dashTimer = 0.3f;
                HasDashed = false;
                

            }
        }

        if (numDash < numDashHave)
        {
            dashRegenTimer += Time.deltaTime;

            if(dashRegenTimer >= 2)
            {
                numDash += 1;
                dashRegenTimer = 0;
                
            }
        }

        if (IsSliding)
        {
            slideTimer -= Time.deltaTime;

            if (slideTimer <= 0 || rb.velocity.x == 0)
            {
                //animator.SetBool(AnimationStrings.crouched, false);
                animator.SetBool("SlideFinish", true);

                IsSliding = false;
                slideTimer = 1f;


            }
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
            slideTimer = 0;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // face left
            IsFacingRight = false;
            slideTimer = 0;
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
        
        else if (context.started && CanWallJump && !CanClimb && CanWallJumpAgain)
        {
            HasWallJumped = true;
            CanWallJumpAgain = false;
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
        if (context.started && !IsRunning)
        {
            animator.SetBool(AnimationStrings.crouched, true);
            IsCrouched = true;
            
        }

        else if (context.started && !IsRunning)
        {
            animator.SetBool(AnimationStrings.crouched, true);
            IsCrouched = true;
            
        }

        else if (context.canceled) //&& !IsSliding)
        {
            animator.SetBool(AnimationStrings.crouched, false);
            IsCrouched = false;
            
        }
        
        else if (context.started && IsRunning)
        {
            animator.SetBool(AnimationStrings.crouched, true);
            animator.SetBool("SlideFinish", false);
            IsSliding = true;
        }

        else if (context.started && IsSuperRunning)
        {
            animator.SetBool(AnimationStrings.crouched, true);
            animator.SetBool("SlideFinish", false);
            IsSliding = true;
        }
        
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Interacted = true;
            Debug.Log("Pressed");
        }

        if(context.canceled)
        {
            Interacted = false;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.started && numDash > 0)
        {
            animator.SetBool("Dash", true);
            HasDashed = true;
        }


    }

}