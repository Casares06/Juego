using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escalada : MonoBehaviour
{
    PlayerController Pc;

    void Start()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
        if(Pc.HasWallJump)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Pc.HasWallJump = true;
            Destroy(gameObject);
        }

    }
}