using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject proyectilePrefab;

    public DetectionZone detectionZone1;
    public DetectionZone detectionZone2;
    public DetectionZone detectionZone3;

    GameObject PC;
    Rigidbody2D rb;
    Animator animator;
    Damageable damageable;

    private bool HasTarget1;
    private bool HasTarget2;
    private bool HasTarget3;

    public int Health;
    public int MaxHealth;

    public EnemyHealthBar HealthBar;
    

    void Awake()
    {
        PC = GameObject.Find("Player");
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

        Health = damageable.Health;
        MaxHealth = damageable.MaxHealth;
        HealthBar.SetHealth(Health, MaxHealth);
    
        HasTarget1 = detectionZone1.DetectedColliders.Count > 0;
        HasTarget2 = detectionZone2.DetectedColliders.Count > 0;
        HasTarget3 = detectionZone3.DetectedColliders.Count > 0;

        FlipDirection();
        AttackAlternate();
           
        
    }

    void Attack1()
    {
        animator.SetBool("Attack1", true);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
    }

    void Attack2()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", true);
        animator.SetBool("Attack3", false);
    }

    void Attack3()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", true);
    }

    
    void AttackAlternate()
    {
        if(HasTarget1)
        {
            Attack1();
        }

        if(HasTarget2)
        {
            Attack2();
        }

        if(HasTarget3)
        {
            Attack3();
        }
    }

    void ResetAnimator()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
    }


    void FlipDirection()
    {
        if (PC.transform.position.x < transform.position.x && damageable.IsAlive)
        {
            transform.localScale = new Vector2(-0.7f, transform.localScale.y);
        }

        if (PC.transform.position.x > transform.position.x && damageable.IsAlive)
        {
            transform.localScale = new Vector2(0.7f, transform.localScale.y);
        }
    }

    public void FireProyectile()
    {
        GameObject proyectile = Instantiate(proyectilePrefab, launchPoint.position, proyectilePrefab.transform.rotation);
        Vector3 origScale = proyectile.transform.localScale;
        proyectile.transform.localScale = new Vector3(transform.localScale.x , origScale.y, origScale.z);
    }
}
