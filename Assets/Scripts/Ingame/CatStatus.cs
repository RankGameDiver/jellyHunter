using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStatus : MonoBehaviour
{
    public struct Cat
    {
        public float health; // 체력
        public float nowHealth;
        public float damage; // 공격력    
        public float defend; // 기본 방어력
        public float nowDefend;
    }

    public Cat cat;
    public Stage stage;
    public CatAnimation catAnimation;
    private SoundM soundM { get { return GetComponent<SoundM>(); } }
    public GameObject GameOver;
    public GameObject buff;
    private bool life; // 생명
    private float defendTime;

    void Start()
    {
        if (GameData.healthItem)
            cat.health = 220;
        else
            cat.health = 200;
        cat.nowHealth = cat.health;
        cat.damage = 10;
        cat.defend = 5;
        defendTime = 0;
        life = true;
    }

    void Update()
    {
        if (cat.nowHealth <= 0 && life) // 플레이어가 죽었을 때 실행
            life = false;
    }

    public void BasicAttack() // 기본 공격 스킬
    {
        if (GameData.attackItem)
            GameData.skillPower += 1;

        float trueDamage = cat.damage * GameData.skillPower + (LenghthCheck() * GameData.skillPower * cat.damage / 5 * 2);

        for (int i = 0; i < 5; i++)
        {
            JellyStatus sJelly = stage.gJelly[i].GetComponent<JellyStatus>();
            if (sJelly.jellyCount <= 1 && stage.gJelly[i].activeInHierarchy)
                sJelly.Attacked(trueDamage);
        }
        GameData.skillPower = 0;
    }

    public void AllAttack() // 전체 공격 스킬
    {
        if (GameData.attackItem) { GameData.skillPower += 1; }
        float trueDamage = cat.damage * GameData.skillPower + (LenghthCheck() * GameData.skillPower * cat.damage / 5);
        for (int i = 0; i < 5; i++)
        {
            JellyStatus sJelly = stage.gJelly[i].GetComponent<JellyStatus>();
            if (stage.gJelly[i].activeInHierarchy)
                sJelly.Attacked(trueDamage);
        }
        GameData.skillPower = 0;
    }

    public void Heal() // 기본 힐 스킬
    {
        if (GameData.healItem) { GameData.skillPower += 1; }
        cat.nowHealth += GameData.skillPower * 10 * LenghthCheck();
        if (cat.nowHealth > cat.health)
            cat.nowHealth = cat.health;
        GameData.skillPower = 0;
    }

    public void Defend()
    {
        if (GameData.defendItem) { GameData.skillPower += 1; }

        if (defendTime <= 0)
            StartCoroutine(Defending());
        else
        defendTime += 5.0f;
    }

    IEnumerator Defending()
    {
        defendTime += 5.0f;
        cat.nowDefend = cat.defend + GameData.skillPower * 5;
        buff.SetActive(true);
        GameData.skillPower = 0;

        Debug.Log("Defending, defend : " + cat.nowDefend);
        StartCoroutine(Timer());
        yield return new WaitUntil(() => { return defendTime <= 0; });

        cat.nowDefend = cat.defend;
        buff.SetActive(false);
        Debug.Log("End Defending, defend : " + cat.nowDefend);
        yield return null;
    }

    IEnumerator Timer()
    {
        while (defendTime > 0)
        {
            defendTime -= 0.5f;
            yield return new WaitForSeconds(0.5f);
            //Debug.Log("defendTime : " + defendTime);
        }
        Debug.Log("End Timer");
        yield return null;
    }

    public int LenghthCheck()
    {
        if (GameData.skillPower <= 2) return 1;
        if (GameData.skillPower <= 4) return 2;
        return 3;
    }

    public void Attacked(float m_damage)
    {
        float tempHealth = cat.nowHealth;
        cat.nowHealth -= m_damage * (1 - cat.defend / 100);
        if (cat.nowHealth > tempHealth) cat.nowHealth = tempHealth;
        catAnimation.Attacked();

        if (cat.nowHealth <= 0)
            GameOver.SetActive(true);
    }

    public float GetHealth()            { return cat.nowHealth; }
    public float GetMaxHP()             { return cat.health; }
    public void SetHealth(float hp)     { cat.health = hp; }
    public float GetDefend()            { return cat.defend; }
    public bool GetLife()               { return life; }
}
