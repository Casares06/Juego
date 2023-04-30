using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public ParticleSystem upgradeEffect;
    public ParticleSystem healthPickupEffect;
    public ParticleSystem moneyEffect;
    public Transform playerPosition;
    
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RealHealer RH = collision.GetComponent<RealHealer>();
        ArrowPickUp ARP = collision.GetComponent<ArrowPickUp>();
        

        if(collision.tag == "upgrade")
        {
            Instantiate(upgradeEffect, playerPosition);
        }

        if(collision.tag == "Healer" && RH.CanTake)
        {
            Instantiate(healthPickupEffect, playerPosition);
        }

        if(collision.tag == "ArrowPickUp" && ARP.CanTakeArrow)
        {
            Instantiate(healthPickupEffect, playerPosition);
        }

        if(collision.tag == "Money")
        {
            Instantiate(moneyEffect, playerPosition);
        }
    }

}
