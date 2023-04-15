using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Secrets : MonoBehaviour
{
    Tilemap renderer;
    
    void Start()
    {
        renderer = GetComponent<Tilemap>();
    }
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            renderer.color = new Color(1f, 1f, 1f, 0.3f);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            renderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
