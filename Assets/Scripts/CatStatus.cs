using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStatus : MonoBehaviour
{
    public int health; // 체력
    public int damage; // 공격력
    public int defend; // 방어력

    public Stage stage;

    public bool shield; // 방어 활성화 체크

    void Start()
    {

    }

    void Update()
    {
        if (health <= 0) // 플레이어가 죽었을 때 실행
        {

        }
    }

    public void Attack()
    {
        for (int i = 0; i < 5; i++)
        {
            if (stage.sJelly[i].jellyCount < 2)
            {
                stage.sJelly[i].health -= (damage + (3 + GameData.skillPower * 3));
            }
        }
        GameData.skillPower = 0;
        Debug.Log("Attack");
    }

    public void Defend()
    {
        if (shield == false)
            shield = true;
        else
            shield = false;
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
