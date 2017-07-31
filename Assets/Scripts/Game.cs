using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static GameObject[] sBlock = new GameObject[8];
    public GameObject currentBlock;

    void Start()
    {
        StartCoroutine(LogicLoop());
        GameData.skill_1 = 0;
        GameData.skill_2 = 0;
        GameData.skill_3 = 0;
    }

    void Update()
    {

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
            sBlock[GameData.blockCount] = currentBlock;
            
            Block block = currentBlock.GetComponent<Block>();

            block.MoveBlock(GameData.blockCount);

            yield return new WaitUntil(() => { return !block.isMoving; });//*
        }

        yield break;    //코루틴 종료시키는 코드
    }

    public void OnAct()
    {
        gameObject.SetActive(true);
    }

    public void OffAct()
    {
        gameObject.SetActive(false);
        GameData.blockCount--;
    }

}
