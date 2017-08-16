using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStatus : MonoBehaviour
{
    public int health; // 체력
    public int damage; // 공격력
    public int defend; // 방어력

    void Start()
    {

    }

    void Update()
    {
        if (health <= 0) // 플레이어가 죽었을 때 실행
        {

        }
    }

}
