using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColdDeath : MonoBehaviour
{
    public UnityEvent<int, Vector2> takeDamage;

    public float freezetimer;
    public float hottimer;
    private float takedamagetimer;
    public float slider = 10;
    public float numberTimer;

    public Color cold;
    public Color hot;

    public Material material;

    Damageable player;
    UIManager UI;

    SpriteRenderer spriterenderer;

    public bool Freezing;
    public bool Freezed;

    public float intensity = 0;

    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Damageable>();
        takedamagetimer = numberTimer;
        

    }



    void Update()
    {
        if(Freezing && freezetimer >= 0 && !Freezed)
        {
            freezetimer -= Time.deltaTime;
            //spriterenderer.material.color = Color.Lerp(spriterenderer.material.color, cold, slider/10000);
            hottimer = 10;

            slider -= Time.deltaTime;

            if (intensity >= 0)
            {
                intensity = -(slider - 10) / 100;//Time.deltaTime/100;
            }           
            
        }
        else if(Freezed && hottimer >= 0 && !Freezing)
        {
            Freezing = false;
            hottimer -= Time.deltaTime;
            //spriterenderer.material.color = Color.Lerp(spriterenderer.material.color, hot, slider/10000);
            freezetimer = 10;
            slider += Time.deltaTime * 5;

            if (intensity >= 0f)
            {
                intensity = -(slider - 10) / 100;//Time.deltaTime/100;
            }

          
        }

        if (slider <= 0f)
        {
            takedamagetimer -= Time.deltaTime;

            if(takedamagetimer <= 0)
            {
                player.Health -= 1;
                CharacterEvents.characterDamaged.Invoke(GameObject.Find("Player"), 1);
                takedamagetimer += numberTimer;
            }
        }

        if(slider < 0)
        {
            slider = 0;
        }
        if(slider > 10)
        {
            slider = 10;
        
        }

        if(intensity < 0)
        {
            intensity = 0f;
        }

        if(intensity > 10)
        {
            intensity = 10;
        
        }



        material.color = new Color (0 * intensity, 131 * intensity, 191 * intensity, 255);
        

    }

    
}
