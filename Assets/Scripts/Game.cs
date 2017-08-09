using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] sBlock; // 모든 블럭의 배열
    public Block[] cBlock; // 모든 블럭 스크립트의 배열
    public GameObject currentBlock; // 현재 생성된 블럭

    public int chainCount = 0;

    void Start()
    {
        StartCoroutine(CreateBlockLoop());
    }

    void Update()
    {
        UsingBlock();
    }

    public void OnAct() // 오브젝트 활성화
    {
        for (int i = 0; i < GameData.blockAmount; i++)
        {
            if (!sBlock[i].activeInHierarchy)
            {
                sBlock[i].SetActive(true);
                sBlock[i].transform.position = (GameData.spawnPos);
                currentBlock = sBlock[i];
                cBlock[i].Init();
                return;
            }
        }
    }

    public void OffAct(int i) // 오브젝트 비활성화
    {
        sBlock[i].SetActive(false);
        GameData.blockCount--;
    }

    IEnumerator CreateBlockLoop()
    {
        while (true)
        {
            yield return Create();//*
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Create() // 블럭 생성
    {
        if (GameData.blockCount < GameData.blockAmount)
        {
            OnAct();

            for (int i = 0; i < 7; i++)
            {
                if (currentBlock == sBlock[i])
                {
                    cBlock[i].MoveBlock();
                    yield return new WaitUntil(() => { return !cBlock[i].isMoving; });//*
                }
            }
        }
        yield break;    //코루틴 종료시키는 코드
    }

    public void chainCheck(int temp) // 블럭 체인 시스템(미완성) // temp는 클릭된 블럭, count는 현재 활성화된 블럭의 개수
    {
        int currentBlock = temp; // 현재 반복문에서 돌고있는 블럭
        int nextBlockNum = cBlock[temp].blockNum; // 터치된 블럭이 가지고 있는 blockNum값(blockNum은 배열이라 0부터 시작)
        int blockCount = GameData.blockCount;

        bool checkRight = true;

        while (checkRight)
        {           
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (!sBlock[j].activeInHierarchy) { }
                    else
                    {
                        if (nextBlockNum - 1 == cBlock[j].blockNum)
                        {
                            if (cBlock[temp].skillNum == cBlock[j].skillNum)
                            {
                                currentBlock = j;
                                nextBlockNum--;
                            }
                            else
                            {
                                checkRight = false;
                            }
                        }
                        else { }
                    }
                    
                }
            }
            checkRight = false;
        }
        Debug.Log(nextBlockNum);

        while (nextBlockNum < blockCount && chainCount < 5) // blockCount는 활성화 된 블럭의 개수이므로 1부터 시작
        {
            if (currentBlock >= 8)
            {
                currentBlock = 0;
                nextBlockNum++;
            }

            if (cBlock[currentBlock].blockNum == nextBlockNum) // cBlock[i]의 활성화된 순서가 j와 같을때
            {
                if (cBlock[currentBlock].skillNum == cBlock[temp].skillNum)
                {
                    chainCount++;
                    OffAct(currentBlock);
                }
                else
                    nextBlockNum += 7;
                currentBlock = 0;
                nextBlockNum++;
            }
            else
                currentBlock++;
        }
    }

    public void BlockNum(int temp) // 비활성화 된 블럭들의 blockNum값을 조절
    {
        int blockNum = cBlock[temp].blockNum;
        for (int i = 0; i < 7; i++)
        {
            if (!sBlock[i].activeInHierarchy)
            {             
                cBlock[i].blockNum = 10;
            }
            else if(sBlock[i].activeInHierarchy && cBlock[i].blockNum > blockNum)
            {
                cBlock[i].blockNum -= chainCount;
                cBlock[i].MoveBlock();
            }
        }
        chainCount = 0;
        GameData.touchBlock = null;
    }

    public void UsingBlock() // 블럭이 사용되어질때
    {
        if (GameData.touchBlock != null) // 무언가 터치되었을때 실행
        {
            for (int i = 0; i < 7; i++)
            {
                if (GameData.touchBlock == sBlock[i]) // 터치된 블럭에 닿으면 실행
                {
                    chainCheck(i);
                    BlockNum(i);
                }
            }
        }
    }

}



