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

    private int skillKind; // 스킬 종류
    private int blockNum; // 활성화 상태일 때 블럭의 순서
    public int blockArr; // 블럭 배열

    private bool isMoving; // 블럭이 생성된 후 움직임을 체크

    public void Init() // 블럭 생성(초기화)
    {
        int index = Random.Range(0, GameData.blockKinds); // 스킬 종류 설정
        blockImg.sprite = skillImg[index];
        skillKind = index;
        blockNum = GameData.blockCount;
        GameData.blockCount++;
    }

    public void MoveBlock()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        if (isMoving) yield return null;
        isMoving = true;

        while (isMoving)
        {
            if (blockPos.anchoredPosition.x >= 200 * (GameData.blockAmount - blockNum - 1) + 150)
                isMoving = false;
            blockPos.anchoredPosition = Vector2.MoveTowards(new Vector2(blockPos.anchoredPosition.x, blockPos.anchoredPosition.y),
                                                            new Vector2(200.0f * (GameData.blockAmount - blockNum - 1) + 150.0f, 0), 40.0f);
            yield return null;
        }
    }

    private void OnMouseDown() //클릭되었을 시
    {
        GameData.touchBlock = gameObject; //블럭 터치됨
        game.UsingBlock(blockArr);
        if (GameData.blockCount < 0)
            GameData.blockCount = 0;
        
    }

    public int GetSkillNum() { return skillKind; }
    public void SetBlockNum(int temp) { blockNum = temp; }
    public int GetBlockNum() { return blockNum; }
    public bool GetIsMoving() { return isMoving; }
}
