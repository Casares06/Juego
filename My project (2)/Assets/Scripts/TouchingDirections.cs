using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallCheckDistance = 0.2f;
    public float ceilingCheckDistance = 0.05f;


    CapsuleCollider2D touchingCol;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private bool _isGrounded = true;

    public bool IsGrounded {get {
        return _isGrounded;
    } private set {
        _isGrounded = value;
        animator.SetBool(AnimationStrings.isGrounded, value);
    }}

    private bool _isOnWall = false;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnWall {get {
        return _isOnWall;
    } private set {
        _isOnWall = value;
        animator.SetBool(AnimationStrings.isOnWall, value);
    }}

    private bool _isOnCeiling = false;

    public bool IsOnCeiling {get {
        return _isOnCeiling;
    } private set {
        _isOnCeiling = value;
        animator.SetBool(AnimationStrings.isOnCeiling, value);
    }}

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallCheckDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingCheckDistance) > 0;
    }

}
