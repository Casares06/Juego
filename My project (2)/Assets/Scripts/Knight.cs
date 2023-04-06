using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float walkAcceleration = 5f;
    public float maxSpeed = 5f;
    public float walkStopRate= 0.02f;
    public float ArrowSpawn;
    public float ArrowCanSpawnTime;
    public DetectionZone detectionZone;
    public DetectionZone cliffdetectionzone;
    public GameObject arrowPickUp;
    public GameObject healer;
    int random;

    Rigidbody2D rb;
    TouchingDirections touchingdirections;
    Animator animator;
    Damageable damageable;
    PlayerController Pc;

    public enum WalkableDirection { Right, Left}

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set { 
            if(_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value; }
    }

    public bool _hasTarget = false;

    public bool HasTarget { get{ return _hasTarget; 
       
        }   
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove 
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.AttackCooldown);
        }
        set
        {
            animator.SetFloat(AnimationStrings.AttackCooldown, Mathf.Max(value, 0));
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingdirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        HasTarget = detectionZone.DetectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
        if (!damageable.IsAlive)
        {
            ArrowSpawn -= Time.deltaTime;
            ArrowCanSpawnTime -= Time.deltaTime;

            if (Pc.arrows < Pc.maxArrows && Pc.healers < Pc.maxHealers && ArrowCanSpawnTime > 0 && ArrowSpawn < 0)
            {
                random = Random.Range(0,3);

                if(random <= 1)
                {
                   Instantiate(arrowPickUp, transform.position, Quaternion.identity);
                   ArrowSpawn += 30;
                }
                else if(random > 1)
                {
                   Instantiate(healer, transform.position, Quaternion.identity);
                   ArrowSpawn += 30;
                }
            }
            else if (Pc.arrows < Pc.maxArrows && Pc.healers == Pc.maxHealers && ArrowSpawn < 0 && ArrowCanSpawnTime > 0)
            {
                Instantiate(arrowPickUp, transform.position, Quaternion.identity);
                ArrowSpawn += 30;
            }

            else if (Pc.arrows == Pc.maxArrows && Pc.healers < Pc.maxHealers && ArrowSpawn < 0 && ArrowCanSpawnTime > 0)
            {
                Instantiate(healer, transform.position, Quaternion.identity);
                ArrowSpawn += 30;
            }
        }

        if(rb.velocity.x == 0)
        {
            animator.SetBool("IsMoving", false);
        }
        else animator.SetBool("IsMoving", true);
    }

    void FixedUpdate()
    {
        if(touchingdirections.IsGrounded && touchingdirections.IsOnWall && damageable.IsAlive)
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if(CanMove && touchingdirections.IsGrounded)
            {
               
               rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);

            } else {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
        
        
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkableDirection.Right && touchingdirections.IsGrounded)
        {
            WalkDirection = WalkableDirection.Left;
        }else if (WalkDirection == WalkableDirection.Left && touchingdirections.IsGrounded)
        {
            WalkDirection = WalkableDirection.Right;
        }else
        {
            Debug.LogError("Current walkable direction is not set");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }

    public void OnCliffDetected()
    {
        if(touchingdirections.IsGrounded && damageable.IsAlive)
        {
            FlipDirection();
        }
    }
}
