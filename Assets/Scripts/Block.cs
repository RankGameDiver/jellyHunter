using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private GameObject touchBlock; // 터치되는 블럭
    
    void Start ()
    {

    }

    public void Create()
    {
        StartCoroutine(Move());
    }

    public void PullBlock(int count)
    {
        if (isMoving == false)
            StartCoroutine(Pull(count));
    }

    void Update()
    {
        TouchBlock();
        ClickBlock();
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
            transform.Translate((Vector2.right * speed).normalized / 4.0f);
            if (transform.position.x >= GameData.maxXPosition.x - GameData.blockCount * 2.0f)
            {
                isMoving = false;
            }
            yield return null;
        }
    }

    IEnumerator Pull(int count)
    {
        pullMoving = true;
        while (pullMoving)
        {
            transform.Translate((Vector2.right * speed).normalized / 4.0f);

            if (transform.position.x >= GameData.maxXPosition.x - count * 2.0f)
            {
                pullMoving = false;
            }
            yield return null;
        }
    }

    private void TouchBlock()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            if (touchPos != null)
            {
                Ray2D ray = new Ray2D(touchPos, Vector2.zero);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider != null) // 수정 필요(blockCount)
                {
                    touchBlock = hit.collider.gameObject;
                    GameData.blockCount -= 1;
                    Destroy(touchBlock);
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
                touchBlock = hit.collider.gameObject;
                GameData.blockCount -= 1;
                GameData.touchblock = true;
                Destroy(touchBlock);
                BlockUpdate();
            }
        }
    }

    private void BlockUpdate() // 블럭 클릭시 블럭 배열을 재배열함
    {
        for (int i = 0; i < 7; i++)
        {
            if (Game.sBlock[i] == null)
            {
                if (i == 6)
                {
                    Game.sBlock[i] = null;
                }
                else
                {
                    Game.sBlock[i] = Game.sBlock[i + 1];
                    Game.sBlock[i + 1] = null;
                }
            }
        }
    }
}
