using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NumberHealersUI : MonoBehaviour
{
    public TMP_Text HealersText;
    PlayerController Pc;

    void Awake()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        HealersText.text = Pc.healers.ToString();
    }


    void Update()
    {
        HealersText.text = Pc.healers.ToString();
    }
}
