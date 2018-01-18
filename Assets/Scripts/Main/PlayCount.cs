using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCount : MonoBehaviour
{
    public Text timeT;
    public Sprite[] lifeWinImg;
    public SpriteRenderer lifeWindow;
    private int cooldown;
    private string systemTime;
    private int hour;
    private int min;
    private int sec;

    void Update()
    {
        //if (GameData.PlayingCount < 5)
        //    timeSet();
        LifeBar();
    }

    public void timeSet()
    {
        hour = System.DateTime.Now.Hour;
        min = System.DateTime.Now.Minute;
        sec = System.DateTime.Now.Second;
        systemTime = hour.ToString() + ":" + min.ToString() + ":" + sec.ToString();
        timeT.text = systemTime;
    }

    public void LifeBar()
    {
        lifeWindow.sprite = lifeWinImg[GameData.PlayingCount];
    }

    public void GameStart()
    {

    }
}
