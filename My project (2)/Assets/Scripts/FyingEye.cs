using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FyingEye : MonoBehaviour
{
    public float flightSpeed = 3f;
    public float waypointReachedDistance = 0.1f;
    public DetectionZone detectionZone;
    public DetectionZone detectionZone1;
    public List<Transform> waypoints;
    public Collider2D deathCollider;

    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;

    int waypointNum = 0;
    Transform nextWaypoint;

    public bool _hasTarget = false;
    public bool _hasTarget1 = false;

    public bool HasTarget { get{ return _hasTarget; 
       
        }   
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    public bool HasTarget1 { get{ return _hasTarget1; 
       
        }   
        set
        {
            _hasTarget1 = value;
            animator.SetBool(AnimationStrings.hasTarget1, value);
        }
    }

    public bool CanMove 
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }
   
    void Update()
    {
        HasTarget = detectionZone.DetectedColliders.Count > 0;
        HasTarget1 = detectionZone1.DetectedColliders.Count > 0;
    }

    void FixedUpdate()
    {
        if(damageable.IsAlive)
        {
            if(CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.gravityScale = 2f;
            rb.velocity = new Vector2(0, rb.velocity.y);
            deathCollider.enabled = true;
        }
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        UpdateDirection();

        rb.velocity = directionToWaypoint * flightSpeed;

        if(distance < waypointReachedDistance)
        {
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        if(transform.localScale.x > 0)
        {
            if(rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            if(rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }

}
