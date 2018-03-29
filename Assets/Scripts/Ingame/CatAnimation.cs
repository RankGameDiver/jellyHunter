using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
    private Animator animator;
    public Animator effect;

    public CatStatus cat; // 스킬 공식 적용용으로 불러옴
    public GameObject effectObj;

    public GameObject GameOver;

    private float catTempHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animations();
        if (!cat.GetLife())
        {
            animator.Play("CatDead");
            effect.Play("NCatDeadEft");
            new WaitForSeconds(1.0f);
            GameOver.SetActive(true);
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
                cat.BasicAttack();
                animator.Play("CatAttack");
                switch (cat.length)
                {
                    case 1:
                        //effectObj.transform.position = new Vector3(-4.47f, -0.83f, -3.0f);
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
                break;
            case 2:
                GameData.lastSkillKind = GameData.skillKind;
                cat.Defend();
                animator.Play("CatDefend");            
                switch (cat.length)
                {
                    case 1:
                        effect.Play("NCatDefend1Eft");
                        break;
                    case 2:
                        effect.Play("NCatDefend2Eft");
                        break;
                    case 3:
                        effect.Play("NCatDefend3Eft");
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                GameData.lastSkillKind = GameData.skillKind;           
                cat.Heal();
                switch (cat.length)
                {
                    case 1:
                        animator.Play("Cat1Heal");
                        effect.Play("NCatHeal1Eft");
                        break;
                    case 2:
                        animator.Play("Cat2Heal");
                        effect.Play("NCatHeal2Eft");
                        break;
                    case 3:
                        animator.Play("Cat2Heal");
                        effect.Play("NCatHeal2Eft");
                        break;
                    default:
                        break;
                }
                break;
            case 4:
                GameData.lastSkillKind = GameData.skillKind;
                cat.AllAttack();
                switch (cat.length)
                {
                    case 1:
                        //animator.Play("Cat1Heal");
                        //effect.Play("NCatHeal1Eft");
                        break;
                    case 2:
                        //animator.Play("Cat2Heal");
                        //effect.Play("NCatHeal2Eft");
                        break;
                    case 3:
                        //animator.Play("Cat2Heal");
                        //effect.Play("NCatHeal2Eft");
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        cat.length = 0;
        GameData.skillKind = 0;
    }

    public void Attacked()
    {
        animator.Play("CatHurt");
        effect.Play("NCatHurtEft");
    }

}
