using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    Sprite[] skillImg;
    SpriteRenderer blockSprite;
    public GameObject game;

    public int skillNum; // 스킬 종류
    public int blockNum; // 활성화 상태일 때 블럭의 순서

    public void Init()
    {
        int index = Random.Range(0, GameData.blockKinds);
        blockSprite = GetComponent<SpriteRenderer>();
        blockSprite.sprite = skillImg[index];
        skillNum = index;
        Vector2 pos = GameData.spawnPos;
        Debug.Log(GameData.blockCount);
        blockNum = GameData.blockCount;
        GameData.blockCount++;
    }

    public void OffAct() // 오브젝트 비활성화
    {
        gameObject.SetActive(false);
        GameData.blockCount--;
    }

    public void MoveBlock()
    {
        StartCoroutine(Move());
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

    IEnumerator Move() // 생성할때
    {
        if (isMoving == false)
        {
            isMoving = true;
            while (isMoving)
            {
                if (transform.position.x > GameData.maxPos.x - blockNum * 2.0f)
                {
                    isMoving = false;
                }
                transform.Translate((Vector2.right * speed).normalized / 4.0f);
                yield return null;
            }
        }
    }

    private void OnMouseDown()
    {
        GameData.touchBlock = gameObject;
        OffAct();
    }

}
