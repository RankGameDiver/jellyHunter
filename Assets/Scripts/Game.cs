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
            Block block = null;
            if (sBlock[i] == null)
            {
                if (delete == false)
                {
                    deleteBlock = i;
                    delete = true;
                }
                Debug.Log(GameData.otherBlock);
                Debug.Log(i);
                sBlock[i] = sBlock[GameData.otherBlock];
                block = sBlock[i].GetComponent<Block>();
                block.blockNum -= GameData.otherBlock - i;
                sBlock[GameData.otherBlock] = null;
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
