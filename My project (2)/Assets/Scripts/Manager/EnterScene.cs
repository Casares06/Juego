using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterScene : MonoBehaviour
{
    public string lastExitName;
    Damageable damageable;
    GameObject player;
    Animator animator;
    Rigidbody2D rb;
    ColdDeath cd;
    public GameObject respawn;
    public GameObject respawnUI;

    Animator animatorUI;
    public float respawnTimer = 3f;
    

    void Start()
    {
        if(PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            PlayerManager.instance.transform.position = transform.position;
        }

        player = GameObject.Find("Player");

        damageable = player.GetComponent<Damageable>();
        animator = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
        cd = player.GetComponent<ColdDeath>();
        animatorUI = respawnUI.GetComponent<Animator>();
        
    }

    void Update()
    {
        if(!damageable.IsAlive)
        {
            respawnTimer -= Time.deltaTime;
            cd.slider = 10;
            animatorUI.SetBool("Respawn", true);
            
            if(respawnTimer <= 0)
            {
                player.transform.position = respawn.transform.position;
                damageable.Health = 100;
                cd.intensity = 0;
                rb.velocity = new Vector2(0,0);
                damageable.IsAlive = true;
                animatorUI.SetBool("Respawn", false);
                
            }
            
        }

        if(damageable.IsAlive)
        {
            respawnTimer = 3f;
        }
    }

}
