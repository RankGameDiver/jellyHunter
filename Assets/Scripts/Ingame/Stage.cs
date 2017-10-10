using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    enum Monster { Normal, Strong, Big };

    int stage = 1; // 스테이지

    public GameObject[] gJelly; // 모든 젤리맨 게임오브젝트 배열

    int jellyKind = 0; // 젤리 종류

    void Start()
    {
        StageKind(stage);
    }

    void StageKind(int stage) // 단계별 스테이지
    {
        switch (stage)
        {
            case 1:
                StartCoroutine(FirstStageLoop());
                break;
            case 2:

                break;
            case 3:

                break;
        }
    }

    IEnumerator FirstStageLoop() // 첫번째 스테이지
    {
        Debug.Log("1 Stage Start");
        yield return CreateLoop(1, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);
        Debug.Log("2 Stage Start");
        yield return CreateLoop(3, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);
        Debug.Log("3 Stage Start");
        yield return CreateLoop(5, (int)Monster.Normal);
        yield break;
    }

    IEnumerator CreateLoop(int num, int jellyKind) // 젤리 생성 루프 (num은 소환하는 횟수)
    {
        for (int i = 0; i < num; i++)
        {
            yield return Create(jellyKind);
            yield return new WaitForSeconds(3f); // 5초 대기
        }
        yield break;
    }

    IEnumerator Create(int jellyKind) // 생성
    {
        for (int i = 0; i < 5; i++) //블럭 갯수만큼 실행
        {
            if (!gJelly[i].activeInHierarchy) //현재 블럭이 활성화 상태가 아니라면
            {
                JellyStatus sJelly = gJelly[i].GetComponent<JellyStatus>();
                gJelly[i].SetActive(true); // 젤리맨 활성화
                gJelly[i].transform.position = new Vector2(6.8f, -0.8f); // 젤리맨 위치를 스폰 위치로 변경
                sJelly.Init();
                sJelly.SetKind(jellyKind); // 젤리맨 스크립트 초기화 (i)안에 다음에 나와야 되는 젤리의 종류를 넣어줘야함
                sJelly.MoveJelly();
                GameData.jellyNum++;
                sJelly.jellyCount = GameData.jellyNum;
                i = 5;
            }
        }
        yield break;
    }

    bool CheckAct() // 현재 활성화된 젤리가 있으면 false를 반환 아니면 true 반환
    {
        for (int i = 0; i < 5; i++)
        {
            if (gJelly[i].activeInHierarchy)
                return false;
            else { }
        }
        return true;
    }
}
