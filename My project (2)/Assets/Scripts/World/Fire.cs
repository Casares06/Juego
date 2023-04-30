using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    ColdDeath coldDeath;

    void Start()
    {
        coldDeath = GameObject.Find("Player").GetComponent<ColdDeath>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            coldDeath.Freezed = true;
            coldDeath.Freezing = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            coldDeath.Freezed = false;
            coldDeath.Freezing = true;
        }
    }
}
