using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickUp : MonoBehaviour
{
    PlayerController Pc;
    public bool CanTakeArrow;

    void Start()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if(Pc.arrows < Pc.maxArrows)
            {
                CanTakeArrow = true;
                Pc.arrows += 3;
                if (Pc.arrows > Pc.maxArrows)
                {
                    Pc.arrows = Pc.maxArrows;
                }
                Destroy(gameObject);
            }
        }

    }
}
