using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(7f, 0);
    public Vector2 knockback = new Vector2(3, 1);
    public int damage = 10;
    public bool gotHit;
    public float destroyTime = 5f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    void Update()
    {
        destroyTime -= Time.deltaTime;
        if(destroyTime < 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        
        if(damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            gotHit = damageable.Hit(damage, deliveredKnockback);
        }

        if(gotHit)
        {
            Destroy(gameObject);
        }

    }

}
