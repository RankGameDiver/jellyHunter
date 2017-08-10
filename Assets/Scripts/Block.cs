using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] //필드 직렬화
    Sprite[] skillImg; //스킬 이미지
    SpriteRenderer blockSprite; //이미지 렌더러
    public GameObject game; //?

    public int skillNum; // 스킬 종류
    public int blockNum; // 활성화 상태일 때 블럭의 순서

    public void Init()
    {
        int index = Random.Range(0, GameData.blockKinds); //스킬 종류 결정
        blockSprite = GetComponent<SpriteRenderer>(); //렌더러 컴포넌트 받아옴
        blockSprite.sprite = skillImg[index]; //인덱스에 따른 스킬 이미지 받아옴
        skillNum = index; //스킬 종류 적용
        Vector2 pos = GameData.spawnPos; //위치=스폰 위치
        blockNum = GameData.blockCount; //현재 소환된 블럭 개수에 따라 블럭 번호 결정
        GameData.blockCount++; //블럭 개수 증가
    }

    public void MoveBlock() //블럭 움직임
    {
        StartCoroutine(Move()); //Move() 코루틴 실행
    }

    private float _speed = 5.0f; //속도 지정
    public float speed
    {
        get
        {
            return _speed * Time.deltaTime; //이동거리 반환
        }
        private set
        {
            _speed = value; //이동속도 변경
        }
    }

    bool _isMoving; // 블럭이 생성된 후 움직임을 체크
    public bool isMoving
    {
        get { return _isMoving; } //움직임 상태 반환
        private set { _isMoving = value; } //움직임 상태 변경
    }

    IEnumerator Move() // 생성할때
    {
        if (isMoving == false) //움직이고 있지 않으면
        {
            isMoving = true; //움직임
            while (isMoving) //움직이는 동안
            {
                if (transform.position.x > GameData.maxPos.x - blockNum * 2.0f) //최대 x좌표에 도달했을 경우
                {
                    isMoving = false; //더 이상 움직이지 않음
                }
                transform.Translate((Vector2.right * speed).normalized / 4.0f); //블럭 이동
                yield return null; //Update문 수행 완료시까지 대기
            }
        }
    }

    private void OnMouseDown() //클릭되었을 시
    {
        GameData.touchBlock = gameObject; //블럭 터치됨
    }

}
