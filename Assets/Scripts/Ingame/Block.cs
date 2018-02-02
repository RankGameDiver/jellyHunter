using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public Sprite[] skillImg; //스킬 이미지
    private Image blockImg { get { return GetComponent<Image>(); } }
    private RectTransform blockPos { get { return GetComponent<RectTransform>(); } }

    public int skillNum; // 스킬 종류
    public int blockNum; // 활성화 상태일 때 블럭의 순서

    public void Init()
    {
        int index = Random.Range(0, GameData.blockKinds); //스킬 종류 결정
        blockImg.sprite = skillImg[index]; //인덱스에 따른 스킬 이미지 받아옴
        skillNum = index; //스킬 종류 적용
        blockNum = GameData.blockCount; //현재 소환된 블럭 개수에 따라 블럭 번호 결정
        GameData.blockCount++; //블럭 개수 증가
    }

    public void MoveBlock() //블럭 움직임
    {
        StartCoroutine(Move()); //Move() 코루틴 실행
    }

    private float _speed = 1.0f; //속도 지정
    public float speed
    {
        get { return _speed * Time.deltaTime; }
        private set { _speed = value; }
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
                //if (transform.position.x > GameData.maxPos.x - blockNum * 1.7200f) //최대 x좌표에 도달했을 경우
                //{
                //    isMoving = false; //더 이상 움직이지 않음
                //}
                //transform.Translate((Vector2.right * speed).normalized / 4.0f); //블럭 이동

                if (blockPos.anchoredPosition.x >= 200 * (GameData.blockAmount - blockNum - 1) + 150)
                {
                    isMoving = false;
                }
                //blockPos.Translate((Vector2.right * speed).normalized);
                blockPos.anchoredPosition = Vector2.MoveTowards(new Vector2(blockPos.anchoredPosition.x, blockPos.anchoredPosition.y), 
                                                                new Vector2(200.0f * (GameData.blockAmount - blockNum - 1) + 150.0f, 0), 40.0f);
                yield return null; //Update문 수행 완료시까지 대기
            }
        }
    }

    private void OnMouseDown() //클릭되었을 시
    {
        GameData.touchBlock = gameObject; //블럭 터치됨
        if (GameData.blockCount < 0)
            GameData.blockCount = 0;
    }

}
