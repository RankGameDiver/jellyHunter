using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    Sprite[] skillImg;
    SpriteRenderer blockSprite;

    public int skillNum; // 스킬 종류
    public int blockNum; // 활성화 상태일 때 블럭의 순서

    void Start()
    {
        int index = Random.Range(0, GameData.blockKinds);
        blockSprite = GetComponent<SpriteRenderer>();
        skillNum = index;
        Vector2 pos = GameData.spawnPos;
        blockNum = GameData.blockCount;
        GameData.blockCount++;
    }

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
                if (transform.position.x > GameData.maxPos.x - blockNum * 1.9f)
                {
                    isMoving = false;
                }
                transform.Translate((Vector2.right * speed).normalized / 4.0f);
                yield return null;
            }
        }
    }
}
