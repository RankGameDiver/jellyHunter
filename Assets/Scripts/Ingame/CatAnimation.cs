using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
    private Animator animator;
    public Animator effect;

    public CatStatus cat; // 스킬 공식 적용용으로 불러옴

    private float catTempHealth;

    void Start()
    {
        catTempHealth = cat.GetHealth();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animations();
        if (cat.GetHealth() <= 0)
        {
            animator.Play("CatDead");
            effect.Play("NCatDeadEft");
        }

        if (cat.GetHealth() < catTempHealth)
        {
            animator.Play("CatHurt");
            effect.Play("NCatHurtEft");
            catTempHealth = cat.GetHealth();
            //Debug.Log("hurt");
        }
    }

    private void animations()
    {
        switch (GameData.skillKind) // skillKind = 스킬 종류 구분
        {
            case 0:
                GameData.lastSkillKind = 0;
                break;
            case 1:
                GameData.lastSkillKind = GameData.skillKind;
                cat.Attack();
                animator.Play("CatAttack");
                switch (cat.length)
                {
                    case 1:
                        effect.Play("NCatAttack1Eft");
                        break;
                    case 2:
                        effect.Play("NCatAttack2Eft");
                        break;
                    case 3:
                        effect.Play("NCatAttack3Eft");
                        break;
                    default:
                        break;
                }          
                GameData.skillKind = 0;
                cat.length = 0;
                break;
            case 2:
                GameData.lastSkillKind = GameData.skillKind;
                animator.Play("CatDefend");
                //effect.Play("NCatDefendEft");
                cat.Defend();
                GameData.skillKind = 0;
                break;
            case 3:
                GameData.lastSkillKind = GameData.skillKind;
                animator.Play("CatHeal");
                //effect.Play("NCatHealEft");
                cat.Heal();
                GameData.skillKind = 0;
                break;
            default:
                break;
        }
    }

}
