using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject curretBlock; // 현재 생성된 블럭

    GameObject[] skillBlocks = new GameObject[GameData.blockKinds];

    class SkillBlock // 미완
    {
        public static GameObject[] sBlock = new GameObject[8]; // 블럭 7개를 배열로 선언해놓음
        public static int skillKind; // 스킬 종류

        public void CreateBlock(int i, GameObject curretBlock, int index)
        {
            sBlock[i] = curretBlock;
            skillKind = index;
        }
    }
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
            checkBlock[i] = SkillBlock.sBlock[i];
    }

    private void BlockUpdate() // 블럭 배열을 재배열함
    {
        for (int i = 0; i < GameData.blockCount; i++)
        {
            if (SkillBlock.sBlock[i] == null)
            {
                if (delete == false)
                {
                    Debug.Log("checkloop");
                    deleteBlock = i;
                    Debug.Log(deleteBlock);
                    delete = true;
                }
                SkillBlock.sBlock[i] = SkillBlock.sBlock[i + 1];
                SkillBlock.sBlock[i + 1] = null;
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

            SkillBlock.sBlock[GameData.blockCount] = curretBlock; // 미완
            SkillBlock.skillKind = index;

            GameData.blockCount += 1;

            Block block = curretBlock.GetComponent<Block>();

            block.Create();

            yield return new WaitUntil(() => { return !block.isMoving; });//*

        }

        yield break;    //코루틴 종료시키는 코드
    }

    public void PullBlock()
    {
        while (GameData.checkTouchblock)
        {
                Debug.Log("deleteBlock");
                for (int i = deleteBlock; i < 7; i++)
                {
                    if (SkillBlock.sBlock[i] != null) // 이부분 수정 필요(null이 아닌곳부터 움직이면 2번째 블럭이 사라졌을때 앞에 블럭까지 적용됨)
                    {
                        Block block = SkillBlock.sBlock[i].GetComponent<Block>();
                        block.PullBlock(i); // i는 지워진 블럭 바로 다음 블럭의 배열
                    }
                }
                delete = false;
                GameData.checkTouchblock = false;
        }
    }

    
}
