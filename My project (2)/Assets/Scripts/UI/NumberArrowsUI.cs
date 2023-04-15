using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NumberArrowsUI : MonoBehaviour
{
    public TMP_Text ArrowsText;
    PlayerController Pc;

    void Awake()
    {
        Pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        ArrowsText.text = Pc.arrows.ToString();
    }

    void Update()
    {
        ArrowsText.text = Pc.arrows.ToString();
    }
}
