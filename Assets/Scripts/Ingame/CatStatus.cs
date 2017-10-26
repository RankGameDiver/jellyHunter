using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStatus : MonoBehaviour
{
    [SerializeField]
    private float health; // 체력
    [SerializeField]
    private float damage; // 공격력    
    [SerializeField]
    private float defend; // 기본 방어력
    private float truedefend;

    public Stage stage;
    public float lastTime; // 방어 적용 시간

    private bool shield; // 방어 활성화 체크
    private bool shieldAct;
    private bool life;

    public int length = 0; // 공격 범위

    void Start()
    {
        health = 100;
        damage = 10;
        defend = 6;
        lastTime = 0;
        life = true;
    }

    void Update()
    {
        if (health <= 0 && life) // 플레이어가 죽었을 때 실행
        {
            
        }
        Defending();
    }

    public int JellyNum(int jellyNum) // jellyNum은 찾아야 하는 젤리의 순서
    {
        for (int i = 0; i < 5; i++)
        {
            JellyStatus sJelly = stage.gJelly[i].GetComponent<JellyStatus>();
            if (sJelly.jellyCount == jellyNum)
                return i;
        }
        return 0;
    }

    public void Attack() // 현재 목표물 지정 오류
    {
        JellyStatus sJelly = stage.gJelly[0].GetComponent<JellyStatus>();
        float trueDamage = (damage + (GameData.skillPower * 4));      

        if (GameData.skillPower >= 1)      length = 1;
        else if (GameData.skillPower >= 3) length = 2;
        else if (GameData.skillPower == 5) length = 3;

        for (int i = 1; i <= length; i++)
        {
            if (stage.gJelly[JellyNum(i)].activeInHierarchy)
            {
                sJelly = stage.gJelly[JellyNum(i)].GetComponent<JellyStatus>();
                sJelly.Attacked(trueDamage);
            }
        }

        GameData.skillPower = 0;
        //Debug.Log("Attack");
    }

    public void Defending() // 방어 버프 적용중
    {
        if (shield && !shieldAct)
        {
            truedefend = defend + (1 + GameData.skillPower);
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
            truedefend = 8;
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
        //Debug.Log("Defend");
    }

    public void Heal()
    {
        health += (10 + GameData.skillPower * 5);
        if (health > 100)
            health = 100;
        //Debug.Log("Heal");
    }

    public void SetHealth(float hp)     { health = hp; }
    public float GetHealth()            { return health; }
    public float GetDefend()            { return truedefend; }
}
