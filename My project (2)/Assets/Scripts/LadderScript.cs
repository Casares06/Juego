using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    PlayerController Pc;

    void Start()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Pc.CanClimb = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Pc.CanClimb = false;
        }
    }
}
