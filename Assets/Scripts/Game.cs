using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject curretBlock; // 현재 생성된 블럭

    GameObject[] skillBlocks = new GameObject[GameData.blockKinds];

    [SerializeField]
    public static GameObject[] sBlock = new GameObject[8]; // 블럭 7개를 배열로 선언해놓음
    public GameObject[] checkBlock = new GameObject[8];

    void Start()
    {
        //ObjectPool
        //Resources.Load<GameObject>("Prefabs/skill_1");
        skillBlocks[0] = Resources.Load<GameObject>("Prefabs/skill_1");
        skillBlocks[1] = Resources.Load<GameObject>("Prefabs/skill_2");
        skillBlocks[2] = Resources.Load<GameObject>("Prefabs/skill_3");
        StartCoroutine(LogicLoop());
        StartCoroutine(CheckLoop());
    }

    void Update()
    {
        for (int i = 0; i < 7; i++)
        {
            checkBlock[i] = sBlock[i];
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

            GameData.blockCount += 1;

            Block block = curretBlock.GetComponent<Block>();

            block.Create();

            yield return new WaitUntil(() => { return !block.isMoving; });//*

        }

        yield break;    //코루틴 종료시키는 코드
    }

    IEnumerator CheckLoop()
    {
        while (true)
        {
            yield return PullBlock();
        }
    }

    IEnumerator PullBlock()
    {
        while (GameData.touchblock)
        {
            for (int i = 0; i < 7; i++)
            {
                if (sBlock[i] != null) // 이부분 수정 필요(null이 아닌곳부터 움직이면 2번째 블럭이 사라졌을때 앞에 블럭까지 적용됨)
                {
                    Block block = sBlock[i].GetComponent<Block>();
                    block.PullBlock(i); // i는 지워진 블럭 바로 다음 블럭의 배열
                }
            }
            GameData.touchblock = false;
            yield return null;
        }
        yield break;
    }
}
