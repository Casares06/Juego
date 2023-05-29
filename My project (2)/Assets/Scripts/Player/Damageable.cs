using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{

    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;

    Animator animator;
    public int _maxHealth = 100;
    public bool gotHit;


    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    public int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    public bool _isAlive = true;
    public bool IsInvincible = false;
    private float timeSinceHit = 0;
    public float InvincibilityTime = 1f;

    public bool IsHit
    {
        get
        {
            return animator.GetBool(AnimationStrings.isHit);
        }
        private set
        {
            animator.SetBool(AnimationStrings.isHit, value);
        }
    }

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(IsInvincible)
        {
            if(timeSinceHit > InvincibilityTime)
            {
                IsInvincible = false;
                  timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }

    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !IsInvincible)
        {
            Health -= damage;
            IsInvincible = true;

            animator.SetTrigger(AnimationStrings.hit);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);

            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            gotHit = true;

            return true;
        }
        gotHit = false;

        return false;

    }
}