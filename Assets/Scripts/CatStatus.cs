using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStatus : MonoBehaviour
{
    int health; // 체력
    int damage; // 공격력

    void Start()
    {
        health = 20;
        damage = 5;
    }

    void Update()
    {
        if (health <= 0) // 플레이어가 죽었을 때 실행
        {

        }
    }

}
