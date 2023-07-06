using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColdSliderUI : MonoBehaviour
{
    ColdDeath CD;

    public Image FillImage;

    void Awake()
    {
        CD = GameObject.Find("Player").GetComponent<ColdDeath>();
    }

    void Update()
    {
        FillImage.fillAmount = CD.slider / 10;
    }
}
