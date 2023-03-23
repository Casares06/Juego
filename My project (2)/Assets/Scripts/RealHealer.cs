using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealHealer : MonoBehaviour
{
    PlayerController Pc;

    public bool CanTake;

    void Start()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            if(Pc.healers < Pc.maxHealers)
            {
                CanTake = true;
                Pc.healers += 1;
                Destroy(gameObject);
                
            }
        }

    }
}
