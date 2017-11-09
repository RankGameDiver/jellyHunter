using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    enum Monster { Normal, Strong, Big };

    int stage = 1; // 스테이지

    public GameObject[] gJelly; // 모든 젤리맨 게임오브젝트 배열

    void Start()
    {
        StageKind(stage);
    }

    void StageKind(int stage) // 단계별 스테이지
    {
        switch (stage)
        {
            case 1:
                StartCoroutine(Stage1());
                break;
            case 2:
                StartCoroutine(Stage2());
                break;
            case 3:
                StartCoroutine(Stage3());
                break;
        }
    }

    IEnumerator Stage1() // 첫번째 스테이지
    {
        yield return CreateLoop(1, (int)Monster.Big);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);

        yield return CreateLoop(3, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);

        yield return CreateLoop(5, (int)Monster.Normal);
        yield break;
    }

    IEnumerator Stage2()
    {
        yield return CreateLoop(3, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);

        yield return CreateLoop(4, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);

        yield return CreateLoop(1, (int)Monster.Strong);
        yield break;
    }

    IEnumerator Stage3()
    {
        yield return CreateLoop(3, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);

        yield return CreateLoop(5, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);

        yield return CreateLoop(1, (int)Monster.Big);
        yield break;
    }

    IEnumerator CreateLoop(int num, int jellyKind) // 젤리 생성 루프 (num은 소환하는 횟수)
    {
        for (int i = 0; i < num; i++)
        {
            yield return Create(jellyKind);
            yield return new WaitForSeconds(3f); // 대기
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
                sJelly.jellyKind = jellyKind;
                sJelly.SetJelly();
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
