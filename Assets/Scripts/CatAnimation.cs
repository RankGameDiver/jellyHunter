using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
    private Animator animator;

    public CatStatus catStatus; // 스킬 공식 적용용으로 불러옴

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animations();
        if (animator.GetBool("SkillReset") == true)
        {
            Skill();
            animator.SetBool("SkillReset", false);
        }
    }

    private void Skill()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Defend", false);
        animator.SetBool("Heal", false);
    }

    IEnumerator animations()
    {
        switch (GameData.skillKind) // skillKind = 스킬 종류 구분
        {
            case 0:
                GameData.lastSkillKind = 0;
                break;
            case 1:
                GameData.lastSkillKind = GameData.skillKind;
                animator.SetBool("Attack", true);
                catStatus.Attack();
                GameData.skillKind = 0;
                break;
            case 2:
                GameData.lastSkillKind = GameData.skillKind;
                animator.SetBool("Defend", true);
                catStatus.Defend();
                GameData.skillKind = 0;
                break;
            case 3:
                GameData.lastSkillKind = GameData.skillKind;
                animator.SetBool("Heal", true);
                GameData.skillKind = 0;
                catStatus.Heal();
                break;
            default:
                break;
        }
    }

}
