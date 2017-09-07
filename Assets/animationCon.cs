using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationCon : StateMachineBehaviour
{
    float lastTime = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {    
        lastTime += Time.deltaTime;
        if (lastTime >= stateInfo.length - 0.05f)
        {
            animator.SetBool("Normal", true);
            animator.SetBool("Attack", false);
            animator.SetBool("Defend", false);
            animator.SetBool("Heal", false);
            Debug.Log("false");
            lastTime = 0.0f;
        }
        //Debug.Log(stateInfo.length);
        //Debug.Log(lastTime);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {    
        //Debug.Log("animationEnd");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
