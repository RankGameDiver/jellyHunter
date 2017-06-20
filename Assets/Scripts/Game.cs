using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject curretBlock; // 현재 생성된 블럭

    GameObject[] skillBlocks = new GameObject[GameData.blockKinds];

    [SerializeField]
    public static GameObject[] sBlock = new GameObject[8]; // 블럭 7개를 배열로 선언해놓음
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
        ClickBlock();
        TouchBlock();
        BlockUpdate();

        if (delete == true)
            PullBlock();

        for (int i = 0; i < 7; i++)
            checkBlock[i] = sBlock[i];
    }

    private void BlockUpdate() // 블럭 배열을 재배열함
    {
        for (int i = 0; i < GameData.blockCount; i++)
        {
            if (sBlock[i] == null)
            {
                if (delete == false)
                {
                    Debug.Log("checkloop");
                    deleteBlock = i;
                    Debug.Log(deleteBlock);
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
                    if (sBlock[i] != null) // 이부분 수정 필요(null이 아닌곳부터 움직이면 2번째 블럭이 사라졌을때 앞에 블럭까지 적용됨)
                    {
                        Block block = sBlock[i].GetComponent<Block>();
                        block.PullBlock(i); // i는 지워진 블럭 바로 다음 블럭의 배열
                    }
                }
                delete = false;
                GameData.checkTouchblock = false;
        }
    }

    private void TouchBlock()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            if (touchPos != null)
            {
                Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray2D ray = new Ray2D(clickPos, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider != null)
                {
                    GameData.touchBlock = hit.collider.gameObject;
                    Destroy(GameData.touchBlock);
                    GameData.blockCount -= 1;
                    GameData.checkTouchblock = true;
                }
            }
        }
    }

    private void ClickBlock() // 테스트 전용 클릭함수
    {      
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(clickPos, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                GameData.touchBlock = hit.collider.gameObject;
                Destroy(GameData.touchBlock);
                GameData.blockCount -= 1;
                GameData.checkTouchblock = true;
            }
        }
    }
}
