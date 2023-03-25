using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivation : MonoBehaviour
{
    PlayerController Pc;
    private GameObject HealerUI;
    private GameObject ArrowsUI;

    void Start()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
        HealerUI = GameObject.FindWithTag("HealerUI");
        ArrowsUI = GameObject.FindWithTag("ArrowsUI");

        HealerUI.SetActive(false);
        ArrowsUI.SetActive(false);
    }

    void Update()
    {
        if (Pc.HasHealers)
        {
            HealerUI.SetActive(true);
        }
        if(Pc.HasBow)
        {
            ArrowsUI.SetActive(true);
        }
    }
}
