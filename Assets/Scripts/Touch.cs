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
                GameData.otherBlock = i + 1;
                int j = i + 1;
                while (j <= GameData.blockCount)
                {
                    if (sBlock[j] != null)
                    {
                        Block CBlock = sBlock[j].GetComponent<Block>();
                        if (block.skillNum == CBlock.skillNum)
                        {
                            GameData.otherBlock += 1;
                            Destroy(sBlock[j]);
                        }
                        else
                        {
                            j = GameData.blockCount;
                        }
                    }

                    if (GameData.otherBlock >= 5)
                    {
                        j = GameData.blockCount;
                    }
                    j++;
                }
                GameData.skillKind = block.skillNum;
                GameData.tempBlock = 0;
                for (int temp = i; temp < GameData.otherBlock; temp++)
                {
                    Destroy(sBlock[temp]);
                    GameData.tempBlock++;
                }
                GameData.blockCount -= GameData.otherBlock;
            }
        }
        GameData.checkTouchblock = true;
    }
}


