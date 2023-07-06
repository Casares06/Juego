using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    TouchingDirections touchingdirections;
    Animator animator;
    Rigidbody2D rb;

    void Start()
    {
        touchingdirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(touchingdirections.IsOnWall)
        {
            animator.SetBool("Explode", true);
            
        }
    }
}
