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
    private float slider = 10;
    public float numberTimer;

    public Color cold;
    public Color hot;

    Damageable player;
    UIManager UI;

    SpriteRenderer spriterenderer;

    public bool Freezing;
    public bool Freezed;

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
            spriterenderer.material.color = Color.Lerp(spriterenderer.material.color, cold, slider/10000);
            hottimer = 10;

            slider -= Time.deltaTime;
            
        }
        else if(Freezed && hottimer >= 0 && !Freezing)
        {
            Freezing = false;
            hottimer -= Time.deltaTime;
            spriterenderer.material.color = Color.Lerp(spriterenderer.material.color, hot, slider/10000);
            freezetimer = 10;
            slider += Time.deltaTime;
        }

        if (slider <= 0)
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

    }

    
}
