using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private GameObject touchBlock; // 터치되는 블럭
    //[SerializeField] new Rigidbody2D rigidbody;
    
    void Start ()
    {

    }

    public void Create()
    {
        StartCoroutine(Move());
    }

    void Update()
    {
        TouchBlock();
    }


    private float _speed = 2.10f;
    public float speed
    {
        get
        {
            return _speed * Time.deltaTime * 10.0f;
        }
        private set
        {
            _speed = value;
        }
    }
	
    bool _isMoving;
    //public int a { get; private set; }
    public bool isMoving
    {
        get { return _isMoving; }
        private set { _isMoving = value; }
    }
    IEnumerator Move()
    {
        isMoving = true;
        while(isMoving)
        {
            transform.Translate(Vector2.right * speed);
            if (transform.position.x >= GameData.maxXPosition - GameData.blockCount * 2.1f)
            {
                isMoving = false;
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
                if (hit.collider != null)
                {
                    touchBlock = hit.collider.gameObject;
                    GameData.blockCount -= 1;
                    Destroy(touchBlock);
                }
            }
        }
    }
}
