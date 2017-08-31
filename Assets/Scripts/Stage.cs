using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    int stage; // 스테이지

    public GameObject[] gJelly; // 모든 젤리맨 게임오브젝트 배열

    int jellyKind = 0; // 젤리 종류

    void Start()
    {
        StartCoroutine(CreateLoop());
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
            yield return new WaitForSeconds(5f); // 5초 대기
        }
    }

    IEnumerator Create() // 생성
    {
        for (int i = 0; i < 5; i++) //블럭 갯수만큼 실행
        {
            if (!gJelly[i].activeInHierarchy) //현재 블럭이 활성화 상태가 아니라면
            {
                JellyStatus sJelly = gJelly[i].GetComponent<JellyStatus>();
                gJelly[i].SetActive(true); // 젤리맨 활성화
                gJelly[i].transform.position = new Vector2(6.8f, -1.7f); // 젤리맨 위치를 스폰 위치로 변경
                sJelly.Init(jellyKind); // 젤리맨 스크립트 초기화 (i)안에 다음에 나와야 되는 젤리의 종류를 넣어줘야함
                sJelly.MoveJelly();
                GameData.jellyNum++;
                sJelly.jellyCount = GameData.jellyNum;
                i = 5;
            }
        }
        yield break;
    }
}
