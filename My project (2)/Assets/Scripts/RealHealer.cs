using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealHealer : MonoBehaviour
{
    PlayerController Pc;

    public bool CanTake;
    private float destroyTime = 15f;

    void Start()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        destroyTime -= Time.deltaTime;

        if(destroyTime <= 0)
        {
            Destroy(gameObject);
        }
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
