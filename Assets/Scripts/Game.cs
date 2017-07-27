using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject curretBlock; // 현재 생성된 블럭

    GameObject[] skillBlocks = new GameObject[GameData.blockKinds];

    public static GameObject[] sBlock = new GameObject[8]; // 블럭 7개를 배열로 선언해놓음
    public static int skillNum; // 스킬 종류

    public GameObject[] checkBlock = new GameObject[8];

    private int deleteBlock; // 지워진 블럭의 배열 값
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

        for (int i = 0; i < 7; i++)
        {
            checkBlock[i] = sBlock[i];
        }
    }

    private void BlockUpdate() // 블럭 배열을 재배열함
    {
        for (int i = 0; i < GameData.blockCount; i++) // 지워진 블럭의 배열값을 delete안에 넣고 배열을 당겨준다
        {
            
            if (sBlock[i] == null)
            {
                if (delete == false)
                {
                    deleteBlock = i;
                    delete = true;
                }

                
                int temp = 0;
                for (int j = deleteBlock; j < GameData.blockCount; j++)
                {
                    Block block = null;
                    sBlock[j + temp] = sBlock[GameData.otherBlock + temp];
                    block = sBlock[j + temp].GetComponent<Block>();
                    block.blockNum -= GameData.tempBlock;
                    temp++;
                }

                //for (int j = 0; deleteBlock + j < GameData.blockCount; j++)
                //{
                //    Debug.Log("otherBlock : " + GameData.otherBlock);
                //    Debug.Log("tempBlock : " + GameData.tempBlock);
                //    Debug.Log("deleteBlock + j : " + deleteBlock + j);
                //    sBlock[deleteBlock + j] = sBlock[GameData.otherBlock + j];
                //    block = sBlock[deleteBlock + j].GetComponent<Block>();
                //    block.blockNum -= GameData.tempBlock;
                //    sBlock[GameData.otherBlock + j] = null;
                //}
            }
        }
        if (delete == true) // PullBlock 넣을 자리
        {
            Block block = null;
            for (int i = deleteBlock; i < GameData.blockCount; i++)
            {
                block = sBlock[i].GetComponent<Block>();
                block.MoveBlock(i);
            }
            delete = false;
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

            block.MoveBlock(GameData.blockCount);

            block.blockNum = GameData.blockCount;

            GameData.blockCount += 1;

            yield return new WaitUntil(() => { return !block.isMoving; });//*

        }

        yield break;    //코루틴 종료시키는 코드
    }

}
