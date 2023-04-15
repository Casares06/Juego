using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiver : MonoBehaviour
{
    PlayerController Pc;

    void Start()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();

        if(Pc.HasQuiver)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Pc.HasQuiver = true;
            Pc.maxArrows += 5;
            Pc.arrows = Pc.maxArrows;
            Destroy(gameObject);

        }

    }
}
