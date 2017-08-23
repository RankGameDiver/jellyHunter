using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStatus : MonoBehaviour
{
    public float health; // 체력
    public float damage; // 공격력
    public float defend; // 방어력(현재 적용 수치)
    public float def; // 기본 방어력

    public Stage stage;

    public float lastTime;

    public bool shield; // 방어 활성화 체크

    void Start()
    {
        lastTime = 0;
    }

    void Update()
    {
        if (health <= 0) // 플레이어가 죽었을 때 실행
        {

        }

        if (shield)
        {
            defend = def + (2 + GameData.skillPower);
            lastTime += Time.deltaTime;
            if (lastTime >= 5.0f)
            {
                shield = false;
                lastTime = 0;
            }
        }
        else
            defend = 8;

    }

    public void Attack()
    {
        float  trueDamage = (damage + (3 + GameData.skillPower * 3));
        for (int i = 0; i < 5; i++)
        {
            if (stage.sJelly[i].jellyCount < 2)
            {
                stage.sJelly[i].health -= (trueDamage - stage.sJelly[i].defend * 1.5f);
            }
        }
        GameData.skillPower = 0;
        Debug.Log("Attack");
    }

    public void Defend()
    {
        if (shield == false)
            shield = true;
        else { }
        Debug.Log("Defend");
    }

    public void Heal()
    {
        health += (10 + GameData.skillPower * 5);
        if (health > 100)
            health = 100;
        Debug.Log("Heal");
    }

}
