using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
    private Animator animator;
    public float timer;
    public float waitTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        timer = 0.0f;
        waitTime = 2.0f;
    }

    void Update()
    {
        animations();
    }

    private void animations()
    {
        switch (GameData.skillKind) // GameData.skillKind: 스킬블럭 상태 받아오는 거 표현한 임시변수
        {
            case 0:
                if (GameData.lastSkillKind != GameData.skillKind)
                {
                    animator.SetInteger("Status", 0);
                    GameData.lastSkillKind = 0;
                }
                break;
            case 1:
                GameData.lastSkillKind = GameData.skillKind;
                while (timer <= waitTime)
                {
                    animator.SetInteger("Status", 1);
                    timer += Time.deltaTime;
                }
                timer = 0;
                GameData.skillKind = 0;
                Debug.Log("skillNum_1");
                break;
            case 2:
                GameData.lastSkillKind = GameData.skillKind;
                while (timer <= waitTime)
                {
                    animator.SetInteger("Status", 2);
                    timer += Time.deltaTime;
                }
                timer = 0;
                GameData.skillKind = 0;
                Debug.Log("skillNum_2");
                break;
            case 3:
                GameData.lastSkillKind = GameData.skillKind;
                while (timer <= waitTime)
                {
                    animator.SetInteger("Status", 3);
                    timer += Time.deltaTime;
                }
                timer = 0;
                GameData.skillKind = 0;
                Debug.Log("skillNum_3");
                break;
            default:
                break;
        }
    }
}
