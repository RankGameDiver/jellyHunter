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

                for (int i = 0; i < GameData.blockCount; i++)
                {
                    if (sBlock[i] == GameData.touchBlock)
                    {
                        for (int j = i; j < GameData.blockCount; j++)
                        {
                            if (sBlock[j] == null)
                                j = i + 5;
                            else if (sBlock[i].GetComponent<Block>().skillNum == sBlock[j].GetComponent<Block>().skillNum)
                            {
                                Destroy(sBlock[j]);
                                GameData.blockCount -= 1;
                            }
                        }
                    }
                }
                GameData.checkTouchblock = true;
            }
        }
    }
}
