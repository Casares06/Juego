using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public TMP_Text CoinsText;
    PlayerController Pc;

    void Awake()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        CoinsText.text = Pc.coins.ToString() + "$";
    }

    void Update()
    {
        CoinsText.text = Pc.coins.ToString() + "$";
    }
}
