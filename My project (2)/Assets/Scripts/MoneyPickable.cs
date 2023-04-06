using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickable : MonoBehaviour
{
    PlayerController Pc;

    void Start()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Pc.coins += 5;
            Destroy(gameObject);

        }

    }
}
