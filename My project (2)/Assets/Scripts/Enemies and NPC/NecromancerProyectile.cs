using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerProyectile : MonoBehaviour
{
    public GameObject necromancerProyectile;
    public GameObject necromancerBolt;
    public GameObject plasmaBall;

    public Transform launcher;
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
    public bool Angry;
    public float hitTimer = 4f;
    public bool Hit = true;
    public int numHit = 0;

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

    public void NecromancerBolts()
    {
        GameObject proyectile1 = Instantiate(necromancerBolt, new Vector2(Random.Range(-49,-56), transform.position.y - 2.6f), necromancerProyectile.transform.rotation);
        GameObject proyectile2 = Instantiate(necromancerBolt, new Vector2(Random.Range(-42,-49), transform.position.y - 2.6f), necromancerProyectile.transform.rotation);
        GameObject proyectile3 = Instantiate(necromancerBolt, new Vector2(Random.Range(-35,-42), transform.position.y - 2.6f), necromancerProyectile.transform.rotation);
        GameObject proyectile4 = Instantiate(necromancerBolt, new Vector2(Random.Range(-29,-35), transform.position.y - 2.6f), necromancerProyectile.transform.rotation);
        

    }

    public void NecromancerPlasmaBalls()
    {
        GameObject proyectile = Instantiate(plasmaBall, new Vector2(launcher.transform.position.x, launcher.transform.position.y -1.5f), plasmaBall.transform.rotation);
        Vector3 origScale = proyectile.transform.localScale;
        proyectile.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.y, origScale.z);

        GameObject proyectile1 = Instantiate(plasmaBall, new Vector2(launcher.transform.position.x, launcher.transform.position.y ), plasmaBall.transform.rotation);
        Vector3 origScale1 = proyectile1.transform.localScale;
        proyectile1.transform.localScale = new Vector3(origScale1.x * transform.localScale.x > 0 ? 1 : -1, origScale1.y, origScale1.z);

    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(0, rb.velocity.y + 0);
        animator.SetTrigger("Hit");
        Hit = true;

    }

    void Update()
    {
        if (Hit)
        {
            numHit += 1;
            Hit = false;
            hitTimer = 4f;

            if(numHit >= 4)
            {
                Angry = true;
            }
        }

        if (hitTimer <= 0)
        {
            numHit = 0;
            Angry = false;
            hitTimer = 4;
        }

        hitTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;

        FlipDirection();

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
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        IsSliding = false;
    }

    void CastDarkBolt()
    {
        animator.SetBool("Attack1", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsMoving", false);
        animator.SetBool("Attack2", false);
        IsSliding = false;
    }

    void CastPlasmaBalls()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsMoving", false);
        animator.SetBool("Attack2", true);
        IsSliding = false;
    }

    void SlideAttack()
    {
        FlipDirection();
        IsSliding = true;
        animator.SetBool("IsMoving", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        
        rb.velocity = new Vector2(slideSpeed * transform.localScale.x, rb.velocity.y);
    }

    void AttackAlternate()
    {
        random = Random.Range(0,4);

        if (random == 0 && !Angry)
        {
            CastLightning();
        }
        else if (random == 1 && !Angry) SlideAttack();
        else if (random == 2 && !Angry)CastDarkBolt();
        else if (random == 3 && !Angry) CastPlasmaBalls();

        if (numHit <= 4 && hitTimer >= 0 && Angry)
        {
            Debug.Log("Angry");
            ResetAnimator();
            SlideAttack();
            numHit = 0;
            attackTimer = 3;
            Angry = false;
        }


        attackTimer += 1;

    }

    void ResetAnimator()
    {
        attackTimer += 1;
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsMoving", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        rb.velocity = new Vector2(0,0);
        IsSliding = false;

    }

    void FlipDirection()
    {
        if (PC.transform.position.x < transform.position.x && !IsSliding)
        {
            transform.localScale = new Vector2(-1.2f, transform.localScale.y);
        }

        if (PC.transform.position.x > transform.position.x && !IsSliding)
        {
            transform.localScale = new Vector2(1.2f, transform.localScale.y);
        }
    }
}
