using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBooleanBehaviour : StateMachineBehaviour
{

    public string boolName;
    public bool updateOnState;
    public bool updateOnStateMachine;
    public bool valueOnEnter, valueOnExit, valueOnDelay;
    public float enableDelay = 0.25f;

    private bool delayDone = false;
    private float timeSinceEntered = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(updateOnState)
       {
         animator.SetBool(boolName, valueOnEnter);

       }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (valueOnDelay && !delayDone)
        {
            timeSinceEntered += Time.deltaTime;
            if (timeSinceEntered > enableDelay)
            {
                animator.SetBool(boolName, valueOnDelay);
                delayDone = true;
            }
        }
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(updateOnState)
       {
         animator.SetBool(boolName, valueOnExit);

       }
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

     
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
        
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }
}
