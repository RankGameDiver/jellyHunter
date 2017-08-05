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
        StartCoroutine(TouchLoop());
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
                block.chaining = true;
            }
        }
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

    IEnumerator TouchLoop()
    {
        while (true)
            yield return UsingBlock();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void chainCheck(int temp) // 블럭 체인 시스템(미완성) // temp는 클릭된 블럭의 위치값, count는 현재 활성화된 블럭의 개수
    {
        Block block;

        for (int i = 0; i < GameData.blockCount; i++)
        {
            for (int j = 0; j < GameData.blockCount;)
            {
                block = cBlock[j];
                if (block.skillNum == cBlock[temp].skillNum)
                {
                    block.chaining = true;
                    chainCount++;
                    GameData.blockCount--;
                    j++;
                }
                else
                {
                    j += 7;
                }
            }
        }
    }

    public void BlockNum() // 비활성화 된 블럭들의 blockNum값을 조절
    {     
        Block block;
        for (int i = 0; i < 7; i++)
        {
            if (!sBlock[i].activeInHierarchy)
            {
                block = sBlock[i].GetComponent<Block>();
                block.chaining = false;
                block.blockNum = 10;
            }
        }
        chainCount = 0;
        GameData.touchBlock = null;
    }

    IEnumerator UsingBlock() // 블럭이 사용되어질때
    {
        if (GameData.touchBlock != null) // 무언가 터치되었을때 실행
        {
            for (int i = 0; i < 7; i++)
            {
                if (GameData.touchBlock == sBlock[i]) // 터치된 블럭에 닿으면 실행
                {
                    int tempNum = cBlock[i].blockNum;
                    chainCheck(i);

                    for (int j = 0; j < GameData.blockCount; j++)
                    {
                        for (int temp = tempNum; temp < GameData.blockCount; temp++)
                        {
                            if (cBlock[j].blockNum == temp)
                            {
                                Debug.Log("check");
                                if (cBlock[j].chaining == true)
                                {
                                    OffAct(sBlock[j]);
                                }
                                else
                                {
                                    cBlock[j].blockNum -= chainCount;
                                    cBlock[j].MoveBlock();
                                }
                                temp++;
                                j -= j;
                            }
                        }
                    }

                }
            }
            BlockNum();
        }
        yield break;
    }

}
