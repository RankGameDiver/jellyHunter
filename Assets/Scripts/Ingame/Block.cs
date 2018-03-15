using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public Sprite[] skillImg; //스킬 이미지
    public Game game;
    private Image blockImg { get { return gameObject.GetComponent<Image>(); } }
    private RectTransform blockPos { get { return GetComponent<RectTransform>(); } }

    private int skillNum; // 스킬 종류
    private int blockNum; // 활성화 상태일 때 블럭의 순서
    public int blockArr; // 블럭 배열 순서

    //private float speed = 1.0f; //속도 지정
    private bool isMoving; // 블럭이 생성된 후 움직임을 체크

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

    IEnumerator Move() // 생성할때
    {
        if (isMoving == false) //움직이고 있지 않으면
        {
            isMoving = true; //움직임
            while (isMoving) //움직이는 동안
            {
                if (blockPos.anchoredPosition.x >= 200 * (GameData.blockAmount - blockNum - 1) + 150)
                {
                    isMoving = false;
                }
                blockPos.anchoredPosition = Vector2.MoveTowards(new Vector2(blockPos.anchoredPosition.x, blockPos.anchoredPosition.y),
                                                                new Vector2(200.0f * (GameData.blockAmount - blockNum - 1) + 150.0f, 0), 40.0f);
                yield return null; //Update문 수행 완료시까지 대기
            }
        }
    }

    private void OnMouseDown() //클릭되었을 시
    {
        GameData.touchBlock = gameObject; //블럭 터치됨
        game.UsingBlock(blockArr);
        if (GameData.blockCount < 0)
            GameData.blockCount = 0;
    }

    public int GetSkillNum() { return skillNum; }
    public void SetBlockNum(int temp) { blockNum = temp; }
    public int GetBlockNum() { return blockNum; }
    public bool GetIsMoving() { return isMoving; }
}
