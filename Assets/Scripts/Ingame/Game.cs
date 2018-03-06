using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] gBlock; // 모든 블럭의 배열
    public Block[] sBlock; // 모든 블럭 스크립트의 배열
    [HideInInspector]
    public GameObject currentBlock; // 현재 생성된 블럭
    public SoundM soundM;

    [HideInInspector]
    public CatStatus catstatus;

    public int chainCount = 0; //연결된 체인 개수

    void Start()
    {
        StartCoroutine(CreateBlockLoop()); //블럭 생성 코루틴
    }

    //void Update()
    //{
    //    UsingBlock(); //블럭 상태 변화 확인
    //}

    public void OnAct() // 블럭 활성화
    {
        for (int i = 0; i < GameData.blockAmount; i++) //블럭 갯수만큼 실행
        {
            if (!gBlock[i].activeInHierarchy) //현재 블럭이 활성화 상태가 아니라면
            {
                gBlock[i].SetActive(true); //블럭 활성화
                gBlock[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                currentBlock = gBlock[i]; //현재 블럭으로 지정
                sBlock[i].Init(); //블럭 스크립트 초기화
                return;
            }
        }
    }

    public void OffAct(int i) // 오브젝트 비활성화
    {
        GameData.skillKind = sBlock[i].GetSkillNum() + 1;
        gBlock[i].SetActive(false); //비활성화
        GameData.blockCount--; //블럭 갯수 감소
    }

    IEnumerator CreateBlockLoop()
    {
        while (true)
        {
            yield return Create(); //* //블럭 생성 코루틴 실행
            yield return new WaitForSeconds(0.5f); //0.5초 대기
        }
    }

    IEnumerator Create() // 블럭 생성
    {
        if (GameData.blockCount < GameData.blockAmount) //현재 필드에 나와있는 블럭 갯수 < 최대 블럭 갯수일 경우
        {
            OnAct(); //블럭 활성화

            for (int i = 0; i < 7; i++) //최대 블럭 갯수만큼 실행
            {
                if (currentBlock == gBlock[i]) //현재 블럭 = gBlock[i]일 경우
                {
                    sBlock[i].MoveBlock(); //블럭 이동
                    yield return new WaitUntil(() => { return !sBlock[i].GetIsMoving(); });//* //블럭 이동 종료시까지 대기
                }
            }
        }
        yield break;    //코루틴 종료시키는 코드
    }

    void ChainStartPos(int number) // 블럭 체인 시스템
    {
        bool checkRight = true; // 오른쪽 -> 왼쪽으로 체크

        while (checkRight) // 체인 체크 시작 위치 조정
        {
            int limit = 1;
            for (int i = sBlock[number].GetBlockNum(); i > 0; i--)
            {
                if (limit >= 5 || number <= 0)
                {
                    checkRight = false;
                }
                else if (sBlock[number].GetSkillNum() == sBlock[FindBlock(sBlock[number].GetBlockNum() - 1)].GetSkillNum())
                {
                    limit++;
                    number--;
                }
            }
        }
        ChainCheck(number); //체인 체크
    }

    void ChainCheck(int currentBlock)
    {
        int limit = 1;
        for (int i = 0; i < 5; i++)
        {
            if (gBlock[i].activeInHierarchy)
            {
                if (sBlock[currentBlock].GetSkillNum() == sBlock[FindBlock(sBlock[currentBlock].GetBlockNum() - i)].GetSkillNum())
                {
                    chainCount++; //체인 카운트 증가
                    limit++;
                    OffAct(currentBlock); //비활성화 함수
                }
                else if (limit >= 5) { i = 5; }
                else { i = 5; }
            }
        }
    }

    void InitBlock(int temp) // 비활성화 된 블럭들의 blockNum값을 조절
    {
        int blockNum = sBlock[temp].GetBlockNum(); //터치된 블럭의 blockNum값
        for (int i = 0; i < 7; i++)
        {
            if (!gBlock[i].activeInHierarchy) //비활성화 상태일 때
            {
                sBlock[i].SetBlockNum(10); //10으로 초기화
            }
            else if (gBlock[i].activeInHierarchy && sBlock[i].GetBlockNum() > blockNum) //현재 체인이 연결된 제일 왼쪽 블럭보다 blockNum 값이 크면
            {
                //sBlock[i].GetBlockNum() -= chainCount; //blockNum 조절
                sBlock[i].SetBlockNum(sBlock[i].GetBlockNum() - chainCount);
                sBlock[i].MoveBlock(); //블럭 이동
            }
        }
        GameData.skillPower = chainCount; //체인 갯수
        soundM.PlaySound(chainCount);
        ScoreManager.PlusChainScore(chainCount);
        chainCount = 0; //체인 카운트 초기화
        GameData.touchBlock = null; //터치 상태 = 터치되지 않음
    }

    public void UsingBlock(int number) // 블럭이 사용되어질때
    {
        ChainStartPos(number); //체인 연결 확인
        InitBlock(number); //blockNum 조절
    }

    public int FindBlock(int blockNum) // 활성화된 블럭의 순서를 인자로 넣으면 그 블럭의 배열 순서를 반환
    {
        for (int i = 0; i < 7; i++)
        {
            if (sBlock[i].GetBlockNum() == blockNum)
            {
                return i;
            }
        }
        return 0;
    }

    public void SetBlock() // 모든 블럭을 비활성화및 초기화
    {
        for (int i = 0; i < 7; i++)
        {
            OffAct(i);
            GameData.skillKind = 0;
            GameData.blockCount = 0;
        }
    }

}