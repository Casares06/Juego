using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject proyectilePrefab;

    public DetectionZone detectionZone1;
    public DetectionZone detectionZone2;
    public DetectionZone spawnDetection;
    public DetectionZone WalkDetection;

    public float walkSpeed = 6f;

    GameObject player;
    Rigidbody2D rb;
    Animator animator;
    Damageable damageable;
    PlayerController PC;

    private bool HasTarget1;
    private bool HasTarget2;
    private bool Spawn;
    private bool WalkZone;

    public int Health;
    public int MaxHealth;

    public EnemyHealthBar HealthBar;

    private float shieldTimer = 2f;

    public bool Shields
    {
        get
        {
            return animator.GetBool("Shield");
        }
    }

    public bool Walk
    {
        get
        {
            return animator.GetBool("Walk");
        }
    }

    public bool Spawned
    {
        get
        {
            return animator.GetBool("Spawn");
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool("lockVelocity");
        }
    }
    

    void Awake()
    {
        player = GameObject.Find("Player");
        PC = player.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }

    void Start()
    {
        Health = damageable.Health;
        MaxHealth = damageable.MaxHealth;
        HealthBar.SetHealth(Health, MaxHealth);
    }

    void Update()
    {
        Movement();

        Health = damageable.Health;
        MaxHealth = damageable.MaxHealth;
        HealthBar.SetHealth(Health, MaxHealth);
    
        HasTarget1 = detectionZone1.DetectedColliders.Count > 0;
        HasTarget2 = detectionZone2.DetectedColliders.Count > 0;
        Spawn = spawnDetection.DetectedColliders.Count > 0;
        WalkZone = WalkDetection.DetectedColliders.Count > 0;

        FlipDirection();
        AttackAlternate();

        if (shieldTimer > 0 && Shields)
        {
            shieldTimer -= Time.deltaTime;
        }

        if(shieldTimer < 0)
        {
           
            shieldTimer = 2f;
        }
           
        
    }

    void Attack1()
    {
        animator.SetBool("Attack1", true);
        animator.SetBool("Attack3", false);
        animator.SetBool("Shield", false);
        
    }


    void Attack3()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack3", true);
        animator.SetBool("Shield", false);
       
    }

    void Shield()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack3", false);
        animator.SetBool("Shield", true);
       
    }

    
    void AttackAlternate()
    {
        if(HasTarget1)
        {
            Attack1();
        }

        if(HasTarget2)
        {
            Attack3();
        }

        if(PC.ReduceArrows && shieldTimer > 0 && Spawn)
        {
            Shield();
        }

        
    }

    void Movement()
    {
        if(Spawn)
        {
            animator.SetBool("Spawn", true);
        }

        if(CanMove)
        {
            rb.velocity = new Vector2(0,0);
        }


        if (WalkZone && Spawned)
        {
            animator.SetBool("Walk", true);

            if(Walk && !CanMove)
            {
                rb.velocity = new Vector2(walkSpeed * transform.localScale.x, rb.velocity.y);
            }

        }
        
        if(!WalkZone)
        {
            animator.SetBool("Walk", false);
            rb.velocity = new Vector2(0, 0);
        }
        if(!damageable.IsAlive)
        {
            rb.velocity = new Vector2(0,0);
        }
    }

    void ResetAnimator()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack3", false);
        animator.SetBool("Shield", false);
        
    }


    void FlipDirection()
    {
        if (player.transform.position.x < transform.position.x && damageable.IsAlive)
        {
            transform.localScale = new Vector2(-0.9f, transform.localScale.y);
        }

        if (player.transform.position.x > transform.position.x && damageable.IsAlive)
        {
            transform.localScale = new Vector2(0.9f, transform.localScale.y);
        }
    }

    public void FireProyectile()
    {
        GameObject proyectile = Instantiate(proyectilePrefab, launchPoint.position, proyectilePrefab.transform.rotation);
        Vector3 origScale = proyectile.transform.localScale;
        proyectile.transform.localScale = new Vector3(transform.localScale.x , origScale.y, origScale.z);
    }
}
