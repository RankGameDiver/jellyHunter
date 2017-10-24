using UnityEngine;
using System.Collections;

public class JellyEffect : JellyStatus
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AttackEft()
    {
        //animator.Play("");
    }
}
