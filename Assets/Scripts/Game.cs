using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] sBlock; // 모든 블럭의 배열
    public Block[] cBlock; // 모든 블럭 스크립트의 배열
    public GameObject currentBlock; // 현재 생성된 블럭

    public CatStatus catstatus;

    public int chainCount = 0; //연결된 체인 개수

    void Start()
    {
        StartCoroutine(CreateBlockLoop()); //블럭 생성 코루틴
    }

    void Update()
    {
        UsingBlock(); //블럭 상태 변화 확인
    }

    public void OnAct() // 오브젝트 활성화
    {
        for (int i = 0; i < GameData.blockAmount; i++) //블럭 갯수만큼 실행
        {
            if (!sBlock[i].activeInHierarchy) //현재 블럭이 활성화 상태가 아니라면
            {
                sBlock[i].SetActive(true); //블럭 활성화
                sBlock[i].transform.position = (GameData.spawnPos); //블럭 위치를 스폰 위치로 변경
                currentBlock = sBlock[i]; //현재 블럭으로 지정
                cBlock[i].Init(); //블럭 스크립트 초기화
                return;
            }
        }
    }

    public void OffAct(int i) // 오브젝트 비활성화
    {
        GameData.skillKind = cBlock[i].skillNum + 1;
        sBlock[i].SetActive(false); //비활성화
        GameData.blockCount--; //블럭 갯수 감소
    }

    IEnumerator CreateBlockLoop()
    {
        while (true)
        {
            yield return Create(); //* //블럭 생성 코루틴 실행
            yield return new WaitForSeconds(1f); //1초 대기
        }
    }

    IEnumerator Create() // 블럭 생성
    {
        if (GameData.blockCount < GameData.blockAmount) //현재 필드에 나와있는 블럭 갯수 < 최대 블럭 갯수일 경우
        {
            OnAct(); //블럭 활성화

            for (int i = 0; i < 7; i++) //최대 블럭 갯수만큼 실행
            {
                if (currentBlock == sBlock[i]) //현재 블럭 = sBlock[i]일 경우
                {
                    cBlock[i].MoveBlock(); //블럭 이동
                    yield return new WaitUntil(() => { return !cBlock[i].isMoving; });//* //블럭 이동 종료시까지 대기
                }
            }
        }
        yield break;    //코루틴 종료시키는 코드
    }

    public void chainStartPos(int temp) // 블럭 체인 시스템 // temp는 클릭된 블럭, count는 현재 활성화된 블럭의 개수
    {
        int currentBlock = temp; // 현재 반복문에서 돌고있는 블럭
        int nextBlockNum = cBlock[temp].blockNum; // 터치된 블럭이 가지고 있는 blockNum값(blockNum은 배열이라 0부터 시작)
        int blockCount = GameData.blockCount; //현재 블럭 갯수
        bool checkRight = true; //오른쪽 -> 왼쪽으로 체크

        while (checkRight) //체인 체크 시작 위치 조정
        {           
            for (int i = 0; i < 7; i++) //최대 블럭 갯수
            {
                for (int j = 0; j < 7; j++) //최대 블럭 갯수
                {
                    if (!sBlock[j].activeInHierarchy) { } //sBlock[j]가 비활성화 상태일 때
                    else
                    {
                        if (nextBlockNum - 1 == cBlock[j].blockNum) //현재 블록 넘버와 일치할 때
                        {
                            if (cBlock[temp].skillNum == cBlock[j].skillNum) //체인 이어질 시
                            {
                                currentBlock = j; //현재 블록 변경
                                nextBlockNum--; //nextBlockNum 감소
                            }
                            else
                            {
                                checkRight = false; //안 이어지므로 종료
                            }
                        }
                    }
                    
                }
            }
            checkRight = false; //체크 종료
        }

        chainCheck(currentBlock, nextBlockNum, blockCount, checkRight, temp); //체인 체크
     
    }

    public void chainCheck(int currentBlock, int nextBlockNum, int blockCount, bool checkRight, int temp)
    {
        while (nextBlockNum < blockCount && chainCount < 5) // blockCount는 활성화 된 블럭의 개수이므로 1부터 시작
        {
            if (currentBlock >= 8) //체크 갯수 오버플로우 시
            {
                currentBlock = 0; //0으로 초기화
                nextBlockNum++;
            }

            if (cBlock[currentBlock].blockNum == nextBlockNum) // cBlock[i]의 활성화된 순서가 j와 같을때
            {
                if (cBlock[currentBlock].skillNum == cBlock[temp].skillNum) //스킬 종류 동일할 시
                {
                    chainCount++; //체인 카운트 증가
                    OffAct(currentBlock); //비활성화 함수
                }
                else
                    nextBlockNum += 7; //while문 밖으로 내보냄
                currentBlock = 0; //0으로 초기화
                nextBlockNum++;
            }
            else
                currentBlock++;
        }
    }

    public void BlockNum(int temp) // 비활성화 된 블럭들의 blockNum값을 조절
    {
        int blockNum = cBlock[temp].blockNum; //터치된 블럭의 blockNum값
        for (int i = 0; i < 7; i++)
        {
            if (!sBlock[i].activeInHierarchy) //비활성화 상태일 때
            {             
                cBlock[i].blockNum = 10; //10으로 초기화
            }
            else if(sBlock[i].activeInHierarchy && cBlock[i].blockNum > blockNum) //현재 체인이 연결된 제일 왼쪽 블럭보다 blockNum 값이 크면
            {
                cBlock[i].blockNum -= chainCount; //blockNum 조절
                cBlock[i].MoveBlock(); //블럭 이동
            }
        }
        GameData.skillPower = chainCount; //체인 갯수
        chainCount = 0; //체인 카운트 초기화
        GameData.touchBlock = null; //터치 상태 = 터치되지 않음
    }

    public void UsingBlock() // 블럭이 사용되어질때
    {
        if (GameData.touchBlock != null) // 무언가 터치되었을때 실행
        {
            for (int i = 0; i < 7; i++) //블럭 최대 갯수만큼 실행
            {
                if (GameData.touchBlock == sBlock[i]) // 터치된 블럭에 닿으면 실행
                {
                    chainStartPos(i); //체인 연결 확인
                    BlockNum(i); //blockNum 조절
                }
            }           
        }
    }

}



