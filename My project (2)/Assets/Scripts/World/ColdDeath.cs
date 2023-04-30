using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdDeath : MonoBehaviour
{
    public float freezetimer;
    public float hottimer;

    public Color cold;
    public Color hot;

    SpriteRenderer spriterenderer;

    public bool Freezing;
    public bool Freezed;

    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }



    void Update()
    {
        if(Freezing && freezetimer >= 0 && !Freezed)
        {
            freezetimer -= Time.deltaTime;
            spriterenderer.material.color = Color.Lerp(spriterenderer.material.color, cold, freezetimer/10000);
            hottimer = 10;
            
        }
        else if(Freezed && hottimer >= 0 && !Freezing)
        {
            Freezing = false;
            hottimer -= Time.deltaTime;
            spriterenderer.material.color = Color.Lerp(spriterenderer.material.color, hot, hottimer/10000);
            freezetimer = 10;
        }
    }

    
}
