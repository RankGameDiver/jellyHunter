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
    private string sTime; // 저장된 시간
    private string nTime; // 현재 시간
    private int coolmin;
    private int coolsec;

    public SaveLoad saveLoad;

    void Start()
    {
        //CoolTime();
        hour = System.DateTime.Now.Hour;
        min = System.DateTime.Now.Minute;
        sec = System.DateTime.Now.Second;
        sTime = FixTimeStr(System.DateTime.Now.Year) + FixTimeStr(System.DateTime.Now.Month) + FixTimeStr(System.DateTime.Now.Day)
                + FixTimeStr(System.DateTime.Now.Hour) + FixTimeStr(System.DateTime.Now.Minute) + FixTimeStr(System.DateTime.Now.Second);

        nTime = FixTimeStr(System.DateTime.Now.Year) + FixTimeStr(System.DateTime.Now.Month) + FixTimeStr(System.DateTime.Now.Day)
                + FixTimeStr(System.DateTime.Now.Hour) + FixTimeStr(System.DateTime.Now.Minute) + FixTimeStr(System.DateTime.Now.Second);

        Debug.Log(sTime);
        LifeStart();
    }

    private string FixTimeStr(int temp)
    {
        if (temp < 10)
            return 0 + temp.ToString();
        else
            return temp.ToString();
    }

    void Update()
    {
        if (sec != System.DateTime.Now.Second && GameData.PlayingCount < 5)
        {
            //CoolTime();
            TimeSet();
            LifeCharge();
        }
        hour = System.DateTime.Now.Hour;
        min = System.DateTime.Now.Minute;
        sec = System.DateTime.Now.Second;
        LifeBar();
    }

    //public void CoolTime() // 화면에 표시
    //{
    //    coolmin = GameData.LifeMin - min;
        
    //    if (coolmin < 0)
    //        coolmin += 1;
    //    coolsec = GameData.LifeSec - sec;
    //    if (coolsec < 0)
    //        coolsec += 60;
    //}

    public void TimeSet()
    {
        systemTime = coolmin.ToString() + ":" + coolsec.ToString();
        timeT.text = systemTime;
    }

    public void LifeBar()
    {
        lifeWindow.sprite = lifeWinImg[GameData.PlayingCount];
    }

    //public void LifeCharge() // 30분
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

    public void LifeCharge() // 테스트용 1분
    {
        /*if (GameData.LifeMin < min || GameData.LifeHour < hour)
        {
            if (GameData.LifeSec == sec)
            {
                GameData.PlayingCount++;
                GameData.LifeMin += 1;
                if (GameData.LifeMin >= 60)
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
                GameData.LifeHour = System.DateTime.Now.Hour;
                GameData.LifeMin = System.DateTime.Now.Minute;
                saveLoad.Save();
            }
        }*/

        int _sTime = int.Parse(sTime);
        int _nTime = int.Parse(nTime);

        if (_sTime + 3000 < _nTime)
        {
            if (GameData.PlayingCount == 5)
            {
                timeT.text = "";
                sTime = "";
            }
            else
            {
                _sTime += 3000;
                GameData.PlayingCount++;
                saveLoad.Save();
            }
        }
    }

    private void LifeStart()
    {
        int _sTime = int.Parse(sTime);
        int _nTime = int.Parse(nTime);

        int temp = _nTime - _sTime;
        if (temp / 3000 > 0)
        {
            GameData.PlayingCount += temp / 3000;
        }
        

        //if (GameData.LifeHour < hour)
        //{
        //    GameData.PlayingCount += (min + 60) - GameData.LifeMin;
        //    GameData.LifeHour = System.DateTime.Now.Hour;
        //    GameData.LifeMin = System.DateTime.Now.Minute;
        //    Debug.Log("LifeStart Hour");
        //}
        //else if (GameData.LifeMin < min)
        //{
        //    GameData.PlayingCount += min - GameData.LifeMin;
        //    GameData.LifeHour = System.DateTime.Now.Hour;
        //    GameData.LifeMin = System.DateTime.Now.Minute;
        //    Debug.Log("LifeStart Min");
        //}

        //if (GameData.PlayingCount >= 5)
        //{
        //    GameData.PlayingCount = 5;
        //    timeT.text = "";
        //    GameData.LifeHour = 0;
        //    GameData.LifeMin = 0;
        //    GameData.LifeSec = 0;
        //    Debug.Log("LifeStart PlayCount >= 5");
        //}
        saveLoad.Save();
    }

}
