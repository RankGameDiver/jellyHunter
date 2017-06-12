using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject curretBlock; // 현재 생성된 블럭
    public int randomBlock; // 랜덤으로 생성되는 블럭

    GameObject[] skillBlocks = new GameObject[GameData.blockKinds];

    private void Start()
    {
        //ObjectPool
        //Resources.Load<GameObject>("Prefabs/skill_1");
        StartCoroutine(LogicLoop());
        skillBlocks[0] = Resources.Load<GameObject>("Prefabs/skill_1");
        skillBlocks[1] = Resources.Load<GameObject>("Prefabs/skill_2");
        skillBlocks[2] = Resources.Load<GameObject>("Prefabs/skill_3");
    }

    IEnumerator LogicLoop()
    {
        while (true)
        {
            yield return Create();//*
            yield return new WaitForSeconds(1f);
        }
    }
    bool isSeven()
    {
        return true;
    }
    IEnumerator Create()
    {
        int index = Random.Range(0,GameData.blockKinds);

        curretBlock = Instantiate(skillBlocks[index],GameData.spawnPos,Quaternion.identity);

        GameData.blockCount += 1;
        
        Block block = curretBlock.GetComponent<Block>();

        block.Create();
        
        yield return new WaitUntil(()=> { return !block.isMoving; });//*

        yield break;    //코루틴 종료시키는 코드
    }
}
