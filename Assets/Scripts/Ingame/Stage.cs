using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    enum Monster { Normal, Strong, Big };

    public GameObject[] gJelly; // 모든 젤리맨 게임오브젝트 배열

    public GameObject gameClear;
    public Game game;

    public Image[] StarImg;
    public int starCount;
    private float alpha = 0;

    void Start()
    {
        StageKind();
        for (int i = 0; i < 3; i++)
        {
            StarImg[i].color = new Color(1, 1, 1, alpha);
        }
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main");
            }
        }

        if (gameClear.activeInHierarchy)
        {
            StarFadeIn();
        }
    }

    private void StarFadeIn()
    {   
        if (StarImg[0].color.a < 1 && ScoreManager.score > 1000) // 20000
        {
            starCount = 1;
            alpha += 0.02f;
            StarImg[0].color = new Color(1, 1, 1, alpha);
        }
        else if (StarImg[1].color.a < 1 && ScoreManager.score > 2000)
        {
            starCount = 2;
            alpha += 0.02f;
            StarImg[1].color = new Color(1, 1, 1, alpha);
        }
        else if (StarImg[2].color.a < 1 && ScoreManager.score > 3000)
        {
            starCount = 3;
            alpha += 0.02f;
            StarImg[2].color = new Color(1, 1, 1, alpha);
        }

        if (alpha > 1)
            alpha = 0;

        switch (GameData.StageNum)
        {
            case 1:
                GameData.Stage1 += starCount;
                if (GameData.Stage1 > 3)
                {
                    GameData.Stage1 = 3;
                }
                break;
            case 2:
                GameData.Stage2 += starCount;
                if (GameData.Stage2 > 3)
                {
                    GameData.Stage2 = 3;
                }
                break;
            case 3:
                GameData.Stage3 += starCount;
                if (GameData.Stage3 > 3)
                {
                    GameData.Stage3 = 3;
                }
                break;
        }
    }

    void StageKind() // 단계별 스테이지
    {
        ScoreManager.score = 0;
        ScoreManager.money = 0;
        ScoreManager.moneyPlus = 0;
        switch (GameData.StageNum)
        {
            case 1:
                StartCoroutine(TestStage());
                //StartCoroutine(Stage1());
                break;
            case 2:
                StartCoroutine(Stage2());
                break;
            case 3:
                StartCoroutine(Stage3());
                break;
        }
    }

    IEnumerator TestStage() // 첫번째 스테이지
    {
        game.SetBlock();
        yield return CreateLoop(1, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        ScoreManager.TimeBonus();
        GameData.Money += ScoreManager.money;
        gameClear.SetActive(true);
        yield break;
    }

    IEnumerator Stage1() // 첫번째 스테이지
    {
        game.SetBlock();
        yield return CreateLoop(1, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);

        yield return CreateLoop(3, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);

        yield return CreateLoop(5, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        ScoreManager.TimeBonus();
        GameData.Money += ScoreManager.money;
        gameClear.SetActive(true);
        yield break;
    }

    IEnumerator Stage2()
    {
        game.SetBlock();
        yield return CreateLoop(3, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);

        yield return CreateLoop(4, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);

        yield return CreateLoop(1, (int)Monster.Strong);
        yield return new WaitUntil(() => { return CheckAct(); });
        ScoreManager.TimeBonus();
        GameData.Money += ScoreManager.money;
        gameClear.SetActive(true);
        yield break;
    }

    IEnumerator Stage3()
    {
        game.SetBlock();
        yield return CreateLoop(3, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);

        yield return CreateLoop(5, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(5f);

        yield return CreateLoop(1, (int)Monster.Big);
        yield return new WaitUntil(() => { return CheckAct(); });
        ScoreManager.TimeBonus();
        GameData.Money += ScoreManager.money;
        gameClear.SetActive(true);
        yield break;
    }

    IEnumerator CreateLoop(int num, int jellyKind) // 젤리 생성 루프 (num은 소환하는 횟수)
    {
        for (int i = 0; i < num; i++)
        {
            yield return Create(jellyKind);
            yield return new WaitForSeconds(3f); // 대기
        }
        yield break;
    }

    IEnumerator Create(int jellyKind) // 생성
    {
        for (int i = 0; i < 5; i++) //블럭 갯수만큼 실행
        {
            if (!gJelly[i].activeInHierarchy) //현재 블럭이 활성화 상태가 아니라면
            {
                JellyStatus sJelly = gJelly[i].GetComponent<JellyStatus>();     
                sJelly.jellyKind = jellyKind;
                sJelly.SetJelly();
                GameData.jellyNum++;
                sJelly.jellyCount = GameData.jellyNum;
                i = 5;
            }
        }
        yield break;
    }

    bool CheckAct() // 현재 활성화된 젤리가 있으면 false를 반환 아니면 true 반환
    {
        for (int i = 0; i < 5; i++)
        {
            if (gJelly[i].activeInHierarchy)
                return false;
            else { }
        }
        return true;
    }
}
