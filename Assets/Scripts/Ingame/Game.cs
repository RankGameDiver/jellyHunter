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

    public int FindBlockArr(int blockNum)
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

    void ChainStartPos(int _blockArr) // 블럭 체인 시스템 // temp는 클릭된 블럭, count는 현재 활성화된 블럭의 개수
    {
        int cBlockArr = _blockArr; // 현재 반복문에서 돌고있는 블럭
        int blockNum = sBlock[_blockArr].GetBlockNum(); // 터치된 블럭이 가지고 있는 blockNum값(blockNum은 배열이라 0부터 시작)
        int blockCount = GameData.blockCount; // 현재 블럭 갯수
        bool checkRight = true; // 오른쪽 -> 왼쪽으로 체크

        while (checkRight) // 체인 체크 시작 위치 조정
        {
            //int limit = 0;
            for (int i = 0; i < 5; i++)
            {
                if (sBlock[cBlockArr].GetBlockNum() - 1 >= 0 && sBlock[cBlockArr].GetSkillNum() == 
                    sBlock[FindBlockArr(sBlock[cBlockArr].GetBlockNum() - 1)].GetSkillNum())
                {
                    cBlockArr = FindBlockArr(sBlock[cBlockArr].GetBlockNum() - 1);
                    blockNum--;
                }
            }

            checkRight = false; //체크 종료
        }
        ChainCheck(cBlockArr, blockNum, blockCount, _blockArr); //체인 체크
    }

    void ChainCheck(int cBlockArr, int blockNum, int blockCount, int temp)
    {
        while (blockNum < blockCount && chainCount < 5) // blockCount는 활성화 된 블럭의 개수이므로 1부터 시작
        {
            if (cBlockArr >= 8) //체크 갯수 오버플로우 시
            {
                cBlockArr = 0; //0으로 초기화
                blockNum++;
            }

            if (sBlock[cBlockArr].GetBlockNum() == blockNum) // sBlock[i]의 활성화된 순서가 j와 같을때
            {
                if (sBlock[cBlockArr].GetSkillNum() == sBlock[temp].GetSkillNum() && sBlock[cBlockArr].GetIsMoving() == false) //스킬 종류 동일할 시
                {
                    chainCount++; //체인 카운트 증가
                    OffAct(cBlockArr); //비활성화 함수
                }
                else
                    blockNum += 7; //while문 밖으로 내보냄
                cBlockArr = 0; //0으로 초기화
                blockNum++;
            }
            else
                cBlockArr++;
        }
    }

    void InitBlock(int temp) // 비활성화 된 블럭들의 blockNum값을 조절
    {
        int blockNum = sBlock[temp].GetBlockNum(); //터치된 블럭의 blockNum값
        for (int i = 0; i < 7; i++)
        {
            if (!gBlock[i].activeInHierarchy) //비활성화 상태일 때
            {
                sBlock[i].SetBlockNum(10);
            }
            else if (gBlock[i].activeInHierarchy && sBlock[i].GetBlockNum() > blockNum) //현재 체인이 연결된 제일 왼쪽 블럭보다 blockNum 값이 크면
            {
                sBlock[i].SetBlockNum(sBlock[i].GetBlockNum() - chainCount);
                sBlock[i].MoveBlock(); //블럭 이동
            }
        }
        GameData.skillPower = chainCount; //체인 갯수
        soundM.PlaySound(chainCount - 1);
        ScoreManager.PlusChainScore(chainCount);
        chainCount = 0; //체인 카운트 초기화
        GameData.touchBlock = null; //터치 상태 = 터치되지 않음
    }

    public void UsingBlock(int blockArr) // 블럭이 사용되어질때
    {
        ChainStartPos(blockArr); //체인 연결 확인
        InitBlock(blockArr); //blockNum 조절
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