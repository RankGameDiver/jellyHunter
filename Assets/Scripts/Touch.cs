using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    void Update()
    {
       //TouchBlock();
       // ClickBlock();
    }

    //private void TouchBlock()
    //{
    //    Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    //    if (touchPos != null)
    //    {
    //        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Ray2D ray = new Ray2D(clickPos, Vector2.zero);
    //        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
    //        if (hit.collider != null)
    //        {
    //            GameData.touchBlock = hit.collider.gameObject;
    //        }
    //    }
    //}

    //private void ClickBlock() // 테스트 전용 클릭함수
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Ray2D ray = new Ray2D(clickPos, Vector2.zero);
    //        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
    //        if (hit.collider != null)
    //        {
    //            GameData.touchBlock = hit.collider.gameObject;
    //        }
    //    }
    //}
}



