using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    private int[] statArr = new int[8];
    public GameObject shopCanvas;

    public GameObject[] statWindow;
    public GameObject[] skillWindow;
    public GameObject tempWindow;

    private bool windowType; // true == statWin, false == skillWin

    public void ExitShop() { shopCanvas.SetActive(false); }
    public void OpenShop() { shopCanvas.SetActive(true); }

    public void SetStat()
    {
        for (int i = 0; i < 3; i++)
        {
            GameData.attackUp = statArr[0];
            GameData.defendUp = statArr[1];
            GameData.healthUp = statArr[2];
        }
    }

    public void GetStat()
    {
        for (int i = 0; i < 3; i++)
        {
            statArr[0] = GameData.attackUp;
            statArr[1] = GameData.defendUp;
            statArr[2] = GameData.healthUp;
        }
    }

    public void Upgrade(int arrKind) // arrKind 0~2까지는 캐릭터 스탯, 3~7까지는 스킬 블록
    {
        GetStat();
        if (statArr[arrKind] < 5 && arrKind < 3)
            statArr[arrKind]++;
        else if (statArr[arrKind] < 5 && arrKind < 8)
            statArr[arrKind]++;
        else { }
        SetStat();
    }

    public void WindowSwap()
    {
        if (windowType == true) // true == statWin, false == skillWin
        {
            statWindow[0].transform.SetSiblingIndex(statWindow[0].transform.GetSiblingIndex() + 1);
            statWindow[0].SetActive(true);
            statWindow[0].GetComponent<Animator>().Play("WinSwap(backStart)");
            skillWindow[0].GetComponent<Animator>().Play("WinSwap(FrontStart)");
            windowType = false;
        }
        else
        {
            skillWindow[0].SetActive(true);
            skillWindow[0].transform.SetSiblingIndex(skillWindow[0].transform.GetSiblingIndex() + 1);
            statWindow[0].GetComponent<Animator>().Play("WinSwap(FrontStart)");
            skillWindow[0].GetComponent<Animator>().Play("WinSwap(backStart)");
            windowType = true;
        }
    }

}
