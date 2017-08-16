using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    int stage; // 스테이지

    public GameObject[] gJelly; // 모든 젤리맨 게임오브젝트 배열
    public JellyStatus[] sJelly; // 모든 젤리맨 스크립트의 배열
    public int health; // 체력
    public int damage; // 공격력
    public int defend; // 방어력

    int jellyKind = 0; // 젤리 종류

    public void NomalJelly()
    {
        health = 50;
        damage = 8;
        defend = 5;
    }

    public void StrongJelly()
    {
        health = 120;
        damage = 15;
        defend = 10;
    }

    public void BigJelly()
    {
        health = 300;
        damage = 25;
        defend = 20;
    }

    void StageKind() // 단계별 스테이지
    {
        switch (stage)
        {
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
        }
    }

    IEnumerator CreateLoop() // 젤리 생성 루프
    {
        while (true)
        {
            yield return Create();
        }
    }

    IEnumerator Create() // 생성
    {
        for (int i = 0; i < 5; i++) //블럭 갯수만큼 실행
        {
            if (!gJelly[i].activeInHierarchy) //현재 블럭이 활성화 상태가 아니라면
            {
                gJelly[i].SetActive(true); //블럭 활성화
                gJelly[i].transform.position = (GameData.spawnPos); //블럭 위치를 스폰 위치로 변경
                sJelly[i].Init(jellyKind); //블럭 스크립트 초기화 (i)안에 다음에 나와야 되는 젤리의 종류를 넣어줘야함 // Init 미완성
            }
        }

        yield break;
    }
}
