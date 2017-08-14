using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyStatus : MonoBehaviour
{
    int health; // 체력
    int damage; // 공격력
    int speed; // 이동속도

    void Start()
    {
        health = 10;
        damage = 4;
        speed = 2;
    }

    void Update()
    {
        if (health <= 0) // 젤리맨이 죽었을 때 실행
        {

        }
    }

}
