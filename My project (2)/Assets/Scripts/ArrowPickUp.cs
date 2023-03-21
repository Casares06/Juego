using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickUp : MonoBehaviour
{
    public ParticleSystem particleEffect;
    public Transform playerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController Pc = collision.GetComponent<PlayerController>();

        Pc.arrows += 3;
        Instantiate(particleEffect, playerPosition);
        Destroy(gameObject);
    }
}
