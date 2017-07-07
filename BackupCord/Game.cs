using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject curretBlock; // 현재 생성된 블럭

    GameObject[] skillBlocks = new GameObject[GameData.blockKinds];

    public static GameObject[] sBlock = new GameObject[8]; // 블럭 7개를 배열로 선언해놓음
    public static int skillNum; // 스킬 종류

    public GameObject[] checkBlock = new GameObject[8];

    private int deleteBlock;
    private bool delete;

    void Start()
    {
        //ObjectPool
        //Resources.Load<GameObject>("Prefabs/skill_1");
        skillBlocks[0] = Resources.Load<GameObject>("Prefabs/skill_1");
        skillBlocks[1] = Resources.Load<GameObject>("Prefabs/skill_2");
        skillBlocks[2] = Resources.Load<GameObject>("Prefabs/skill_3");
        StartCoroutine(LogicLoop());
        delete = false;
    }

    void Update()
    {
        BlockUpdate();

        if (delete == true)
            PullBlock();

        for (int i = 0; i < 7; i++)
        {
            checkBlock[i] = sBlock[i];
        }
    }

    private void BlockUpdate() // 블럭 배열을 재배열함
    {
        for (int i = 0; i < GameData.blockCount; i++)
        {
            if (sBlock[i] == null)
            {              
                if (delete == false)
                {
                    deleteBlock = i;
                    delete = true;
                }
                sBlock[i] = sBlock[i + 1];
                sBlock[i + 1] = null;
            }
            else
            { }
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
        if (GameData.blockCount < 7)
        {
            int index = Random.Range(0, GameData.blockKinds);

            curretBlock = Instantiate(skillBlocks[index], GameData.spawnPos, Quaternion.identity);

            sBlock[GameData.blockCount] = curretBlock;
            
            Block block = curretBlock.GetComponent<Block>();

            block.MoveBlock();

            GameData.blockCount += 1;

            yield return new WaitUntil(() => { return !block.isMoving; });//*

        }

        yield break;    //코루틴 종료시키는 코드
    }

    public void PullBlock()
    {
        while (GameData.checkTouchblock)
        {
            for (int i = deleteBlock; i < 7; i++)
            {
                if (sBlock[i] != null)
                {
                    Block block = sBlock[i].GetComponent<Block>();
                    block.PullBlock(i); // i는 지워진 블럭 바로 다음 블럭의 배열
                }
            }
            delete = false;
            GameData.checkTouchblock = false;
        }
    }


}
