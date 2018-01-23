﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static int score = 0;
    private static int prevScore = 0;

    public Text myText;
    //static string scoreStr;

    private static float timeNow = 0;

    public static int money = 0;
    public static int moneyPlus = 0;

    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timeNow = timeNow + Time.deltaTime;
        if (prevScore != score)
        {
            //scoreStr = " " + score;
        }
        prevScore = score;
    }

    public static void PlusChainScore(int chain)
    {
        score += (chain * 75) * chain;
        PlusMoney();
    }

    public static void PlusDefeatScore(int monsterKind)
    {
        switch (monsterKind)
        {
            case 0: // 웨이브 통과
                score += 5000;
                moneyPlus += 50;
                break;
            case 1: // 잡몹
                score += 3000;
                moneyPlus += 30;
                break;
            case 2: // 중보스
                score += 10000;
                moneyPlus += 100;
                break;
            case 3: // 최종보스
                score += 20000;
                moneyPlus += 200;
                break;
        }
        PlusMoney();
    }

    private static void PlusMoney()
    {
        money = score / 100 + moneyPlus;
    }

    public static void TimeBonus()
    {
        money += 50 * (10 - ((int)timeNow / 30));
    }
}