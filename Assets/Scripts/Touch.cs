using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    public static GameObject[] sBlock = new GameObject[8];

    void Update()
    {
        TouchBlock();
        ClickBlock();
        for (int i = 0; i < 8; i++)
        {
            sBlock[i] = Game.sBlock[i];
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
                Deleting();
            }
        }
    }

    private void Deleting()
    {
        for (int i = 0; i < GameData.blockCount; i++) // 생성되어 있는 블럭만큼 반복
        {
            if (sBlock[i] == GameData.touchBlock) // sBlock[i]와 터치된 블럭이 같으면 실행
            {
                Block block = sBlock[i].GetComponent<Block>();
                if (i != GameData.blockCount) // 가장 최근에 생긴 블럭을 제외한 다른 블럭을 터치했을때
                {
                    GameData.otherBlock = 0;
                    for (int j = 1; j <= GameData.blockCount - i - 1; j++)
                    {
                        Block CBlock = sBlock[i + j].GetComponent<Block>();                      
                        if (CBlock.skillNum == block.skillNum)
                        {
                            Destroy(sBlock[i + j]);
                            GameData.blockCount -= 1;
                        }
                        else
                        {
                            GameData.otherBlock = i + j - 1;
                        }
                    }
                    if (GameData.otherBlock == 0)
                        GameData.otherBlock = GameData.blockCount - 1;
                }
                GameData.skillKind = block.skillNum;
                Destroy(sBlock[i]);
                GameData.blockCount -= 1;
            }
        }
        GameData.checkTouchblock = true;
    }
}


