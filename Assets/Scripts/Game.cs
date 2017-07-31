using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] sBlock;
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
        gameObject.SetActive(true);
    }

    public void OffAct() // 오브젝트 비활성화
    {
        gameObject.SetActive(false);
        GameData.blockCount--;
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

}
