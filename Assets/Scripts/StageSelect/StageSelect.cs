using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public Sprite[] stageNum;
    public Image[] stageImg;

    void Start()
    {
        ChangeStar();
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
    }

    public void Back()
    {
        SceneManager.LoadScene("Main");
    }

    public void ChangeStar()
    {
        stageImg[0].sprite = stageNum[GameData.StageT];
        stageImg[1].sprite = stageNum[GameData.Stage1];
        stageImg[2].sprite = stageNum[GameData.Stage2];
        stageImg[3].sprite = stageNum[GameData.Stage3];
        stageImg[4].sprite = stageNum[GameData.ExStage];
    }

    public void GameStart()
    {
        if (GameData.PlayingCount > 0)
        {
            if (GameData.StageNum == 0)
            {
                SceneManager.LoadScene("Tutorial");
            }
            else
            {
                switch (GameData.StageNum)
                {
                    case 1:
                        if (GameData.Stage1 > 0)
                        {
                            TimeSet();
                            GameData.PlayingCount -= 1;
                            //Debug.Log(GameData.PlayingCount);
                            SceneManager.LoadScene("Ingame");
                        }
                        else
                        {
                            SceneManager.LoadScene("Main");
                        }
                        break;
                    case 2:
                        if (GameData.Stage2 > 0)
                        {
                            TimeSet();
                            GameData.PlayingCount -= 1;
                            Debug.Log(GameData.PlayingCount);
                            SceneManager.LoadScene("Ingame");
                        }
                        else
                        {
                            SceneManager.LoadScene("Main");
                        }
                        break;
                    case 3:
                        if (GameData.Stage3 > 0)
                        {
                            TimeSet();
                            GameData.PlayingCount -= 1;
                            Debug.Log(GameData.PlayingCount);
                            SceneManager.LoadScene("Ingame");
                        }
                        else
                        {
                            SceneManager.LoadScene("Main");
                        }
                        break;
                    case 4:
                        if (GameData.ExStage > 0)
                        {
                            TimeSet();
                            GameData.PlayingCount -= 1;
                            Debug.Log(GameData.PlayingCount);
                            SceneManager.LoadScene("Ingame");
                        }
                        else
                        {
                            SceneManager.LoadScene("Main");
                        }
                        break;
                }
            }
        }
        else
        {
            Debug.Log("No Life!!");
        }
    }

    public void TimeSet()
    {
        PlayCount playCount = new PlayCount();
        if (GameData.sTime == 0)
        {
            GameData.sTime = int.Parse(playCount.FixTimeStr(System.DateTime.Now.Month) + playCount.FixTimeStr(System.DateTime.Now.Day)
                + playCount.FixTimeStr(System.DateTime.Now.Hour) + playCount.FixTimeStr(System.DateTime.Now.Minute) + playCount.FixTimeStr(System.DateTime.Now.Second));
        }
    }
}
