using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerProyectile : MonoBehaviour
{
    public GameObject necromancerProyectile;
    GameObject PC;
    Rigidbody2D rb;
    Animator animator;
    public EnemyHealthBar HealthBar;
    Damageable damageable;

    public float slideSpeed = 20f;

    public int Health;
    public int MaxHealth;
    private int random;

    private bool IsSliding;

    private float attackTimer = 3f;

    void Awake()
    {
        PC = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    public void NecromancerLightning()
    {
        GameObject proyectile = Instantiate(necromancerProyectile, new Vector2(PC.transform.position.x, transform.position.y - 1), necromancerProyectile.transform.rotation);
        //Vector3 origScale = proyectile.transform.localScale;
        //proyectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.y, origScale.z);

    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(0, rb.velocity.y + 0);
        animator.SetTrigger("Hit");

    }

    void Update()
    {
        attackTimer -= Time.deltaTime;

        if (PC.transform.position.x < transform.position.x && !IsSliding)
        {
            transform.localScale = new Vector2(-1.2f, transform.localScale.y);
        }

        if (PC.transform.position.x > transform.position.x && !IsSliding)
        {
            transform.localScale = new Vector2(1.2f, transform.localScale.y);
        }

        Health = damageable.Health;
        MaxHealth = damageable.MaxHealth;
        HealthBar.SetHealth(Health, MaxHealth);

        if (attackTimer <= 0)
        {
            AttackAlternate();
        }

        

        
    }

    void CastLightning()
    {
        animator.SetBool("IsAttacking", true);
        animator.SetBool("IsMoving", false);
    }

    void SlideAttack()
    {
        IsSliding = true;
        animator.SetBool("IsMoving", true);
        animator.SetBool("IsAttacking", false);
        rb.velocity = new Vector2(slideSpeed * transform.localScale.x, rb.velocity.y);
    }

    void AttackAlternate()
    {
        random = Random.Range(0,2);

        if (random == 0)
        {
            CastLightning();
        }
        else SlideAttack();


        attackTimer += 1;

    }

    void ResetAnimator()
    {
        attackTimer += 1;
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsMoving", false);
        rb.velocity = new Vector2(0,0);
        IsSliding = false;
    }
}
