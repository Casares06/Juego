using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    PlayerController PC;
    public Image Circle;

    public float progress = 0f;

    void Awake()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        progress = 0.56f;
        
    }

    void Update()
    {
        Circle.fillAmount = progress;

        
        if(PC.numDash == 0)
        {
            progress = 0f;
        }

        if(PC.numDash == 1)
        {
            progress = 0.25f;
        }

        if(PC.numDash == 2)
        {  
            progress = 0.5f;
        }

        if(PC.numDash == 3)
        {
            progress = 0.75f;
        }

        if(PC.numDash == 4)
        {
            progress = 1;
        }

        if (PC.numDash < PC.numDashHave)
        {
            progress += PC.dashRegenTimer / 8;
        }
    
      
    }
}
