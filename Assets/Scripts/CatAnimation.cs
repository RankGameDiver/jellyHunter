using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
    private Animator animator;
    public float timer;
    public float waitTime;
    public float checkTime;

    public CatStatus catStatus;

    void Start()
    {
        animator = GetComponent<Animator>();
        timer = 0.0f;
        waitTime = 1.0f;
    }

    void Update()
    {
        //animations();
        checkTime = Time.deltaTime;
    }

    private void Skill()
    {
        animator.SetBool("Skill_1", false);
        animator.SetBool("Skill_2", false);
        animator.SetBool("Skill_3", false);
    }

    private void animations()
    {
        switch (GameData.skillKind) // GameData.skillKind: 스킬블럭 상태 받아오는 거 표현한 임시변수
        {
            case 0:
                    GameData.lastSkillKind = 0;
                break;
            case 1:
                GameData.lastSkillKind = GameData.skillKind;
                animator.SetBool("Skill_1", true);
                catStatus.Attack();
                GameData.skillKind = 0;
                timer = 0;
                break;
            case 2:
                GameData.lastSkillKind = GameData.skillKind;
                animator.SetBool("Skill_2", true);
                catStatus.Defend();
                GameData.skillKind = 0;
                timer = 0;
                break;
            case 3:
                GameData.lastSkillKind = GameData.skillKind;
                animator.SetBool("Skill_3", true);
                GameData.skillKind = 0;
                catStatus.Heal();
                timer = 0;
                break;
            default:
                break;
        }
    }
}
