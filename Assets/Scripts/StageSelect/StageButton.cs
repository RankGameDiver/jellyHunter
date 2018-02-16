using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    public int stageNum;

    public void StageSet()
    {
        GameData.StageNum = stageNum;
    }
}
