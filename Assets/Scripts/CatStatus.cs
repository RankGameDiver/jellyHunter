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
    public float lastTime; // 방어 적용 시간

    public bool shield; // 방어 활성화 체크

    public bool shieldAct;

    void Start()
    {
        lastTime = 0;
    }

    void Update()
    {
        if (health <= 0) // 플레이어가 죽었을 때 실행
        {

        }

        Defending();
    }

    public void Attack()
    {
        float trueDamage = (damage + (GameData.skillPower * 4));
        for (int i = 0; i < 3; i++)
        {
            JellyStatus sJelly = stage.gJelly[i].GetComponent<JellyStatus>();

            if (GameData.skillPower <= 1)
            {
                sJelly.health -= (trueDamage * 2 - sJelly.defend * 1.5f);
            }
            else if (GameData.skillPower <= 4)
            {
                if (stage.gJelly[i].activeInHierarchy)
                    sJelly.health -= (trueDamage * 2 - sJelly.defend * 1.5f);
            }
            else
            {
                if (stage.gJelly[i].activeInHierarchy)
                    sJelly.health -= (trueDamage * 2 - sJelly.defend * 1.5f);
            }
        }
        
        GameData.skillPower = 0;
        Debug.Log("Attack");
    }

    public void Defending() // 방어 버프 적용중
    {
        if (shield && !shieldAct)
        {
            defend = def + (1 + GameData.skillPower);
            shieldAct = true;
        }
        else if (shield && shieldAct)
        {
            lastTime += Time.deltaTime;
            if (lastTime >= 5.0f)
            {
                shield = false;
                lastTime = 0;
            }
        }
        else
        {
            defend = 8;
            shieldAct = false;
        }
    }

    public void Defend() // 방어중인지 체크
    {
        if (shield == false)
            shield = true;
        else
        {
            shieldAct = false;
            lastTime = 0;
        }
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
