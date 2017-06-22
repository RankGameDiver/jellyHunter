using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int skillNum; // 스킬 종류

    public void MoveBlock()
    {
        StartCoroutine(Move());
    }

    public void PullBlock(int count)
    {
        if (isMoving == false)
            StartCoroutine(Pull(count));
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

    bool _pullMoving; // 한번 멈춘 블럭이 다시 움직이게함

    public bool pullMoving
    {
        get { return _pullMoving; }
        private set { _pullMoving = value; }
    }

    IEnumerator Move()
    {
        isMoving = true;
        while (isMoving)
        {
            if (transform.position.x >= GameData.maxXPosition.x - GameData.blockCount * 2.0f)
            {
                isMoving = false;
            }
            transform.Translate((Vector2.right * speed).normalized / 4.0f);
            yield return null;
        }
    }

    IEnumerator Pull(int count)
    {
        pullMoving = true;
        while (pullMoving)
        {
            if (transform.position.x >= GameData.maxXPosition.x - (count + 1) * 2.0f)
                pullMoving = false;

            transform.Translate((Vector2.right * speed).normalized / 4.0f);
            yield return null;
        }
    }
}
