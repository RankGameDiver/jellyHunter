using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    private int[] statArr;

    public void GetStat()
    {
        for (int i = 0; i < 3; i++)
        {
            statArr[0] = GameData.attackUp;
            statArr[1] = GameData.defendUp;
            statArr[2] = GameData.healthUp;
        }
    }

    public void SetStat()
    {
        for (int i = 0; i < 3; i++)
        {
            GameData.attackUp = statArr[0];
            GameData.defendUp = statArr[1];
            GameData.healthUp = statArr[2];
        }
    }

    public void Upgrade(int arrKind)
    {
        if (statArr[arrKind] < 5 && arrKind < 3)
        {
            statArr[arrKind]++;
        }
        else { }
    }

}
