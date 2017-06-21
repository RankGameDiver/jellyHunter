using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour {
    private Animator animator;
    float timer;
    int waitTime;

	// Use this for initialization
	void Start () {
        timer = 0.0f;
        waitTime = 2;
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (CatState) // CatState: 스킬블럭 상태 받아오는 거 표현한 임시변수
        {
            case 0:
                if(lastCatState!=CatState)
                {
                    animator.SetInteger("Status", 0);
                    lastCatState = 0;
                }
            case 1:
                lastCatState = CatState; //이전 상태 보존
                while(timer>=waitTime)
                {
                    animator.SetInteger("Status", 1);
                    timer += Time.deltaTime;
                }
                timer = 0;
                CatState = 0;
                break;
            case 2:
                lastCatState = CatState;
                while (timer >= waitTime)
                {
                    animator.SetInteger("Status", 2);
                    timer += Time.deltaTime;
                }
                timer = 0;
                CatState = 0;
                break;
            case 3:
                lastCatState = CatState;
                while (timer >= waitTime)
                {
                    animator.SetInteger("Status", 3);
                    timer += Time.deltaTime;
                }
                timer = 0;
                CatState = 0;
                break;
            default:

        }
	}
}
