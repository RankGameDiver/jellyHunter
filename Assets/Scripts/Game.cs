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
        StartCoroutine(LogicLoop());
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

    public void OffAct(GameObject obj) // 오브젝트 비활성화
    {
        Block block = obj.GetComponent<Block>();
        for (int i = 0; i < 7; i++)
        {
            if (block == cBlock[i])
            {
                obj.SetActive(false);
                GameData.blockCount--;
                block.blockNum--;
                block.chaining = false;
            }
        }
    }

    IEnumerator LogicLoop()
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

    public void chainCheck(int temp) // 블럭 체인 시스템(미완성)
    {
        Block block = cBlock[temp];
        for (int i = 0; i < 7; i++)
        {
            if (block.skillNum == cBlock[i].skillNum)
            {
                cBlock[i].chaining = true;
                chainCount++;
            }
            else
                return;
        }
    }

    public void UsingBlock() // 블럭이 사용되어질때
    {
        if (GameData.touchBlock != null) // 무언가 터치되었을때 실행
        {
            for (int i = 0; i < 7; i++)
            {
                if (GameData.touchBlock == sBlock[i]) // 터치된 블럭에 닿으면 실행
                {
                    int tempNum = cBlock[i].blockNum + 1;
                    //chainCheck(i);
                    for (int j = 0; j < 7; j++)
                    {
                        for (int a = 0; a < 7; a++)
                        {
                            if (cBlock[a].blockNum == tempNum)
                            {
                                if (cBlock[a].chaining)
                                    OffAct(sBlock[a]);
                                else
                                {
                                    cBlock[a].blockNum -= (1 + chainCount);
                                    cBlock[a].MoveBlock();
                                }
                                tempNum++;
                            }
                        }
                    }
                }
            }
            // 비활성화 된 블럭들의 blockNum값을 조절
            Block block;
            for (int i = 0; i < 7; i++)
            {
                if (!sBlock[i].activeInHierarchy)
                {
                    block = sBlock[i].GetComponent<Block>();
                    block.blockNum = 10;
                }
            }
            chainCount = 0;
            GameData.touchBlock = null;
        }
    }

}
