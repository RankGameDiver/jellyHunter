using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static int score = 0;
    private static int prevScore = 0;

    public Text myText;
    static string scoreStr;

    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (prevScore != score)
        {
            scoreStr = " " + score;
            PrintScore();
            Debug.Log(score);
        }
        prevScore = score;
    }

    public static void PrintScore()
    {
 //       myText.GetComponent<Text>().text = scoreStr;
    }

    public static void PlusChainScore(int chain)
    {
        score += (chain * 75) * chain;
    }

    public static void PlusDefeatScore(int monsterKind)
    {
        switch (monsterKind)
        {
            case 0: // 웨이브 통과
                score += 5000;
                break;
            case 1: // 잡몹
                score += 3000;
                break;
            case 2: // 중보스
                score += 10000;
                break;
            case 3: // 최종보스
                score += 20000;
                break;
        }
    }

    public static void TimeBonus(int timePassed)
    {
        score += 5000 * (10 - (timePassed / 30));
    }
}