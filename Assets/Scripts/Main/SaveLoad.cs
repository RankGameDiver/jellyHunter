﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class SaveLoad : MonoBehaviour
{
    [SerializeField]
    private List<Data> S_Data = new List<Data>();

    void Start()
    {
        Load();
    }

    public void Save()
    {
        S_Data.Clear();
        S_Data.Add(new Data(GameData.Money, GameData.StageT, GameData.Stage1, GameData.Stage2, GameData.Stage3, GameData.ExStage, 
                            GameData.PlayingCount, GameData.sTime, GameData.attackUp, GameData.defendUp, GameData.healthUp));
        JsonData charData = JsonMapper.ToJson(S_Data);
        File.WriteAllText(Application.dataPath + "/Data/SaveData.json", charData.ToString());
    }

    public void Load()
    {
        string L_sData = null;
        if (File.Exists(Application.dataPath + "/Data/SaveData.json"))
        {
            L_sData = File.ReadAllText(Application.dataPath + "/Data/SaveData.json");
            JsonData charData = JsonMapper.ToObject(L_sData);        
            GetData(charData);
        }
        else
            Save();
    }

    public void GetData(JsonData data)
    {
        GameData.Money = (int)data[0]["money"];
        GameData.PlayingCount = (int)data[0]["playingCount"];
        GameData.sTime = (int)data[0]["sTime"];
        GameData.StageT = (int)data[0]["stageT"];
        GameData.Stage1 = (int)data[0]["stage1"];
        GameData.Stage2 = (int)data[0]["stage2"];
        GameData.Stage3 = (int)data[0]["stage3"];
        GameData.ExStage = (int)data[0]["exStage"];
        GameData.attackUp = (int)data[0]["attackUp"];
        GameData.defendUp = (int)data[0]["defendUp"];
        GameData.healthUp = (int)data[0]["healthUp"];
    }
}
