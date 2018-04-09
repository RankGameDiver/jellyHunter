using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCount : MonoBehaviour
{
    public Text timeT;
    public Sprite[] lifeWinImg;
    public Image lifeWindow;
    private string systemTime;
    private string nTime; // 현재 시간
    private int coolTime;
    private int deltaTime = 10;
    private int sec;

    public SaveLoad saveLoad;

    void Start()
    {
        sec = System.DateTime.Now.Second;
        nTime = FixTimeStr(System.DateTime.Now.Month) + FixTimeStr(System.DateTime.Now.Day)
                + FixTimeStr(System.DateTime.Now.Hour) + FixTimeStr(System.DateTime.Now.Minute) + FixTimeStr(System.DateTime.Now.Second);
        LifeStart();
    }

    void Update()
    {
        if (sec != System.DateTime.Now.Second)
        {
            nTime = FixTimeStr(System.DateTime.Now.Month) + FixTimeStr(System.DateTime.Now.Day)
               + FixTimeStr(System.DateTime.Now.Hour) + FixTimeStr(System.DateTime.Now.Minute)
               + FixTimeStr(System.DateTime.Now.Second);
            TimeSet();
            LifeCharge();
        }
        sec = System.DateTime.Now.Second;
        lifeWindow.sprite = lifeWinImg[GameData.PlayingCount];
    }

    public string FixTimeStr(int temp)
    {
        if (temp < 10)
            return 0 + temp.ToString();
        else
            return temp.ToString();
    }

    public void TimeSet()
    {
        if (GameData.PlayingCount < 5)
        {
            coolTime = deltaTime - (int.Parse(nTime) - GameData.sTime);
            systemTime = (coolTime / 100).ToString() + ":" + (coolTime % 100 + 1).ToString();
            timeT.text = systemTime;
        }
        else
        {
            coolTime = 0;
            systemTime = "";
            timeT.text = systemTime;
        }
    }

    public void LifeCharge()
    {
        if ((GameData.sTime + deltaTime) < int.Parse(nTime) && GameData.sTime > 0)
        {
            if (GameData.PlayingCount >= 5)
            {
                timeT.text = "";
                GameData.PlayingCount = 5;
                GameData.sTime = 0;
                coolTime = 0;
            }
            else
            {
                GameData.sTime += deltaTime;
                GameData.PlayingCount++;
                saveLoad.Save();
            }
        }
    }

    private void LifeStart()
    { 
        int temp = int.Parse(nTime) - GameData.sTime;
        if ((temp / deltaTime) > 0 && GameData.sTime > 0)
        {
            GameData.PlayingCount += temp / deltaTime;
            if (GameData.PlayingCount > 5)
            {
                GameData.PlayingCount = 5;
                GameData.sTime = 0;
                coolTime = 0;
            }
        }
        saveLoad.Save();
    }

}
