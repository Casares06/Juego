using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    public int KnightAttackDamage = 15;
    public Vector2 knockback = Vector2.zero;
    public bool gotHit;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        
        if(damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            gotHit = damageable.Hit(KnightAttackDamage, deliveredKnockback);
        }

        if(gotHit)
        {
            Debug.Log("IHIHHIHIHIHIHIH");
        }

    }

}
