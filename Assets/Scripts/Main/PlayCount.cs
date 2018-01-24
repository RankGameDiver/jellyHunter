using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCount : MonoBehaviour
{
    public Text timeT;
    public Sprite[] lifeWinImg;
    public SpriteRenderer lifeWindow;
    private string systemTime;
    private int hour;
    private int min;
    private int sec;
    private int coolmin;
    private int coolsec;

 //   public SaveLoad saveLoad;

    void Update()
    {
        if (sec != System.DateTime.Now.Second && GameData.PlayingCount < 5)
        {
            CoolTime();
            timeSet();
            lifeCharge();
        }
        hour = System.DateTime.Now.Hour;
        min = System.DateTime.Now.Minute;
        sec = System.DateTime.Now.Second;

        LifeBar();
    }

    public void CoolTime()
    {
        coolmin = GameData.LifeMin - System.DateTime.Now.Minute;
        if (coolmin < 0)
            coolmin += 1;
        coolsec = GameData.LifeSec - System.DateTime.Now.Second;
        if (coolsec < 0)
            coolsec += 60;
    }

    public void timeSet()
    {
        systemTime = coolmin.ToString() + ":" + coolsec.ToString();
        timeT.text = systemTime;
    }

    public void LifeBar()
    {
        lifeWindow.sprite = lifeWinImg[GameData.PlayingCount];
    }

    //public void lifeCharge() // 30분
    //{
    //    if (GameData.LifeMin + 30 < System.DateTime.Now.Minute || GameData.LifeHour < System.DateTime.Now.Hour && GameData.LifeMin == System.DateTime.Now.Minute)
    //    {
    //        if (GameData.LifeSec == sec)
    //        {
    //            GameData.PlayingCount++;
    //            if (GameData.PlayingCount == 5)
    //            {
    //                timeT.text = "";
    //                GameData.LifeHour = 0;
    //                GameData.LifeMin = 0;
    //                GameData.LifeSec = 0;
    //                coolmin = 30;
    //                coolsec = 0;
    //            }
    //            GameData.LifeMin += 30;
    //            saveLoad.Save();
    //            if (GameData.LifeMin <= 60)
    //            {
    //                GameData.LifeMin -= 30;
    //                GameData.LifeHour++;
    //            }
    //        }
    //    }
    //}

    public void lifeCharge() // 테스트용 1분
    {
        if (GameData.LifeMin < min || GameData.LifeHour < hour)
        {
            if (GameData.LifeSec == sec)
            {
                GameData.PlayingCount++;
                GameData.LifeMin += 1;
                if (GameData.LifeMin <= 60)
                {
                    GameData.LifeMin = 0;
                    GameData.LifeHour++;
                }
                if (GameData.PlayingCount == 5)
                {
                    timeT.text = "";
                    GameData.LifeHour = 0;
                    GameData.LifeMin = 0;
                    GameData.LifeSec = 0;
                }
 //               saveLoad.Save();
            }
        }
    }

}
