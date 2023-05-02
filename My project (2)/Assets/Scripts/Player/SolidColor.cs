using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidColor : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    private Shader myMaterial;
    public Color _color;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myMaterial = Shader.Find("GUI/Text Shader");
        DontDestroyOnLoad(gameObject);
    }

    void ColorSprite()
    {
        myRenderer.material.shader = myMaterial;
        myRenderer.color = _color;
    }

    public void Finish()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        ColorSprite();
    }
}
