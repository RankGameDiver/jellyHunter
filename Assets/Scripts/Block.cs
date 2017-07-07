using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int skillNum; // 스킬 종류
    public int blockNum; // 블럭의 배열 순서

    public void MoveBlock(int count)
    {
        StartCoroutine(Move(count));
    }

    private float _speed = 5.0f;
    public float speed
    {
        get
        {
            return _speed * Time.deltaTime;
        }
        private set
        {
            _speed = value;
        }
    }

    bool _isMoving; // 블럭이 생성된 후 움직임을 체크

    public bool isMoving
    {
        get { return _isMoving; }
        private set { _isMoving = value; }
    }

    IEnumerator Move(int count)
    {
        if (isMoving == false)
        {
            isMoving = true;
            while (isMoving)
            {
                if (transform.position.x >= GameData.maxXPosition.x - (count + 1) * 2.0f)
                {
                    isMoving = false;
                }
                transform.Translate((Vector2.right * speed).normalized / 4.0f);
                yield return null;
            }
        }
        else // 움직이고 있는 블럭은 여기로 들어옴 //  미완성
        {

        }
    }
}
