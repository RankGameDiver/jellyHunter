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
    }

    public void StageIn(int stageNum)
    {
        GameData.StageNum = stageNum;
        if (stageNum == 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene("Ingame");
        }
    }
}
