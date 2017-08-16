using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyStatus : MonoBehaviour
{
    public int health; // 체력
    public int damage; // 공격력
    public int defend; // 방어력
    int speed; // 이동속도

    void Start()
    {

    }

    void Update()
    {
        if (health <= 0) // 젤리맨이 죽었을 때 실행
        {
            gameObject.SetActive(false);
        }
    }

    public void Init(int temp)
    {

    }
}
