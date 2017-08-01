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

    }

    public void OnAct() // 오브젝트 활성화
    {
        for (int i = 0; i < GameData.blockAmount; i++)
        {
            if (!sBlock[i].activeInHierarchy)
            {
                sBlock[i].SetActive(true);
                sBlock[i].transform.position = (GameData.spawnPos);
                currentBlock = sBlock[i] ;
                return;
            }
        }
    }

    public void OffAct() // 오브젝트 비활성화
    {
        gameObject.SetActive(false);
        GameData.blockCount--;
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject);
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
        if (GameData.blockCount < 7)
        {
            OnAct();

            for (int i = 0; i < 7; i++)
            {
                if (currentBlock == sBlock[i])
                {
                    cBlock[i].MoveBlock(GameData.blockCount);
                    yield return new WaitUntil(() => { return !cBlock[i].isMoving; });//*
                }
            }
        }

        yield break;    //코루틴 종료시키는 코드
    }

}
