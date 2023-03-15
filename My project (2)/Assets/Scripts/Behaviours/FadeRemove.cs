using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemove : StateMachineBehaviour
{

    public float fadeTime = 0.5f;

    private float TimeElapsed = 0f;
    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    Color startColor;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TimeElapsed = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TimeElapsed += Time.deltaTime;

        float newAlpha = startColor.a * (1 - TimeElapsed / fadeTime);

        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

        if(TimeElapsed > fadeTime)
        {
            Destroy(objToRemove);
        }
    }


}
