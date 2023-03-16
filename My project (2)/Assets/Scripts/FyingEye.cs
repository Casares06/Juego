using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FyingEye : MonoBehaviour
{
    public DetectionZone detectionZone;

    Animator animator;
    Rigidbody2D rb;

    public bool _hasTarget = false;

    public bool HasTarget { get{ return _hasTarget; 
       
        }   
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
   
    void Update()
    {
        HasTarget = detectionZone.DetectedColliders.Count > 0;
    }
}
