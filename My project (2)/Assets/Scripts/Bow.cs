using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    PlayerController Px;

    void Start()
    {
        Px = GameObject.Find("Player").GetComponent<PlayerController>();

        if (Px.HasBow)
        {
            Destroy(gameObject);
        } 
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController Pc = collision.GetComponent<PlayerController>();

        if (collision.tag == "Player")
        {
            Pc.HasBow = true;
            Destroy(gameObject);
        }
    }
}
