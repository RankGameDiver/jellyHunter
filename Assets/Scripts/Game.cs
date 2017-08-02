using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] sBlock;
    public Block[] cBlock;
    public GameObject currentBlock;

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

    IEnumerator LogicLoop()
    {
        while (true)
        {
            yield return Create();//*
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator Create()
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

    public void UsingBlock()
    {
        if (GameData.touchBlock != null)
        {
            for (int i = 0; i < 7; i++)
            {
                if (GameData.touchBlock == sBlock[i])
                {
                    int temp = cBlock[i].blockNum + 1;
                    for (int j=0; j < 7; j++)
                    {
                        for (int a = 0; a < 7; a++)
                        {
                            if (cBlock[a].blockNum == temp)
                            {
                                cBlock[a].blockNum--;
                                cBlock[a].MoveBlock();
                                temp++;
                            }
                        } 
                    }
                }
            }
            GameData.touchBlock = null;
        }
    }

}
