using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    enum Monster { Normal, Strong, Big, Bomb };

    public GameObject[] gJelly; // 모든 젤리맨 게임오브젝트 배열

    public GameObject gameClear;
    public Game game;
    public GameObject[] button;

    public Image[] StarImg;
    public int starCount;
    private float alpha = 0;

    private int exLv = 1;

    void Start()
    {
        StageKind();
        for (int i = 0; i < 3; i++)
            StarImg[i].color = new Color(1, 1, 1, alpha);
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
                SceneManager.LoadScene("Main");
        }

        if (gameClear.activeInHierarchy)
            StarFadeIn();
    }

    private void StarFadeIn()
    {
        if (StarImg[0].color.a < 1 && ScoreManager.score > 1000) // 20000
        {
            starCount = 2;
            alpha += 0.02f;
            StarImg[0].color = new Color(1, 1, 1, alpha);
        }
        else if (StarImg[1].color.a < 1 && ScoreManager.score > 2000)
        {
            starCount = 3;
            alpha += 0.02f;
            StarImg[1].color = new Color(1, 1, 1, alpha);
        }
        else if (StarImg[2].color.a < 1 && ScoreManager.score > 3000)
        {
            starCount = 4;
            alpha += 0.02f;
            StarImg[2].color = new Color(1, 1, 1, alpha);
        }

        if (alpha > 1)
            alpha = 0;

        switch (GameData.StageNum)
        {
            case 1:
                GameData.Stage1 = starCount;
                if (GameData.Stage2 == 0)
                    GameData.Stage2 = 1;
                break;
            case 2:
                GameData.Stage2 = starCount;
                if (GameData.Stage3 == 0)
                    GameData.Stage3 = 1;
                break;
            case 3:
                GameData.Stage3 = starCount;
                if (GameData.ExStage == 0) { }
                GameData.ExStage = 1;
                break;
            case 4:
                GameData.ExStage = 4;
                break;
        }
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
            case 4:
                StartCoroutine(ExStage());
                break;
        }
    }

    IEnumerator Create(int jellyKind) // 젤리 생성
    {
        for (int i = 0; i < 5; i++)
        {
            if (!gJelly[i].activeInHierarchy)
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

    IEnumerator CreateLoop(int num, int jellyKind) // 젤리 생성 루프 (num은 소환하는 횟수)
    {
        for (int i = 0; i < num; i++)
        {
            yield return Create(jellyKind);
            yield return new WaitForSeconds(3f); // 대기
        }
        yield break;
    }

    IEnumerator TestStage() // 첫번째 스테이지
    {
        game.SetBlock();
        yield return CreateLoop(1, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        StageClear();
        yield break;
    }

    IEnumerator Stage1() // 첫번째 스테이지
    {
        game.SetBlock();
        yield return CreateLoop(1, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(3f);

        yield return CreateLoop(3, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(3f);

        yield return CreateLoop(5, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        StageClear();
        yield break;
    }

    IEnumerator Stage2()
    {
        game.SetBlock();
        //yield return CreateLoop(3, (int)Monster.Normal);
        //yield return new WaitUntil(() => { return CheckAct(); });
        //yield return new WaitForSeconds(3f);

        //yield return CreateLoop(4, (int)Monster.Normal);
        //yield return new WaitUntil(() => { return CheckAct(); });
        //yield return new WaitForSeconds(3f);

        yield return CreateLoop(1, (int)Monster.Strong);
        yield return new WaitUntil(() => { return CheckAct(); });
        StageClear();
        yield break;
    }

    IEnumerator Stage3()
    {
        game.SetBlock();
        //yield return CreateLoop(3, (int)Monster.Normal);
        //yield return new WaitUntil(() => { return CheckAct(); });
        //yield return new WaitForSeconds(3f);

        //yield return CreateLoop(5, (int)Monster.Normal);
        //yield return new WaitUntil(() => { return CheckAct(); });
        //yield return new WaitForSeconds(3f);

        yield return CreateLoop(1, (int)Monster.Big);
        yield return new WaitUntil(() => { return CheckAct(); });
        StageClear();
        yield break;
    }

    IEnumerator ExStage()
    {
        game.SetBlock();
        yield return CreateLoop(3, (int)Monster.Normal);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(3f);

        yield return CreateLoop(3, (int)Monster.Normal);
        yield return CreateLoop(1, (int)Monster.Strong);
        yield return new WaitUntil(() => { return CheckAct(); });
        yield return new WaitForSeconds(3f);

        yield return CreateLoop(3, (int)Monster.Strong);
        yield return CreateLoop(1, (int)Monster.Big);
        yield return new WaitUntil(() => { return CheckAct(); });
        ExClear();
    }

    public void ExClear()
    {
        button[0].SetActive(true);
        button[1].SetActive(true);
    }

    public void ExNext() // temp는 회차
    {
        button[0].SetActive(false);
        button[1].SetActive(false);
        exLv++;
        StartCoroutine(ExStage());
    }

    public void ExEnd()
    {
        button[0].SetActive(false);
        button[1].SetActive(false);
        StageClear();
    }

    private void StageClear()
    {
        ScoreManager.TimeBonus();
        if (GameData.moneyUp)
            GameData.Money += ScoreManager.money + (ScoreManager.money / 2);
        else
            GameData.Money += ScoreManager.money;
        gameClear.SetActive(true);
        GameData.attackUp = false;
        GameData.defendUp = false;
        GameData.healUp = false;
        GameData.moneyUp = false;
        GameData.hpUp = false;
        exLv = 1;
    }
}
