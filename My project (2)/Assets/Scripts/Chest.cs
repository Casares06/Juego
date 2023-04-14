using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    PlayerController Pc;
    Animator animator;
    public GameObject reward;
    private bool CanOpen;
    private Transform position;
    private bool IsOpen = false;
    public float delayTime = 1f;
    private bool Delay = false;

    void Start()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        position = GetComponent<Transform>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CanOpen = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CanOpen = false;
        }

    }

    void Update()
    {
        if (Delay && delayTime > 0)
            {
                delayTime -= Time.deltaTime;
            } 

        if (CanOpen && Pc.Interacted)
        {
            animator.SetBool("Open", true);
            Delay = true;
            Pc.Interacted = false;
        }
        if (delayTime <= 0 && !IsOpen)
            {
                Instantiate(reward, position);
                IsOpen = true;
            }
    }
}
