using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{


   public Image fillImage;
   public Image damageFillImage;
   // [SerializeField] private Image fillImage;
    //[SerializeField] private Image damageFillImage;
    public float healthTimer = 1f;
    private float shrinkSpeed = 0.5f;

    Damageable playerDamageable;

    PlayerController PC;
    
    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PC = player.GetComponent<PlayerController>();
        playerDamageable = player.GetComponent<Damageable>();

        if (player == null)
        {
            Debug.Log("Put the player tag");
        }
    }
    void Start()
    {
        fillImage.fillAmount = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        damageFillImage.fillAmount = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
    }

    void Update()
    {
        fillImage.fillAmount = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthTimer -= Time.deltaTime;

        if (healthTimer < 0) 
        {
            if(fillImage.fillAmount < damageFillImage.fillAmount)
            {
                damageFillImage.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
                
        }

        if(fillImage.fillAmount > damageFillImage.fillAmount)
        {
            damageFillImage.fillAmount = fillImage.fillAmount;
        }

        if(playerDamageable.gotHit)
        {
            healthTimer = 1f;
            playerDamageable.gotHit = false;
        }


        
    }
    

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
}
