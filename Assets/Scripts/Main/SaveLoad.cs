using System.Collections;
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
        S_Data.Add(new Data(GameData.Money, GameData.StageT, GameData.Stage1, GameData.Stage2, GameData.Stage3,
                            GameData.ExStage, GameData.PlayingCount, GameData.LifeHour, GameData.LifeMin, GameData.LifeSec));
        JsonData charData = JsonMapper.ToJson(S_Data);
        File.WriteAllText(Application.dataPath + "/Data/SaveData.json", charData.ToString());
    }

    public void Load()
    {
        string L_sData = null;
        if (File.Exists(Application.dataPath + "/Data/SaveData.json"))
        {
            L_sData = File.ReadAllText(Application.dataPath + "/Data/SaveData.json");
            // Debug.Log(L_sData);
            JsonData charData = JsonMapper.ToObject(L_sData); // string 형식이 아니라 다른 방식으로 불러오게 변환         
            GetData(charData);
        }
        else
        {
            Save();
        }

    }

    public void GetData(JsonData data)
    {
        GameData.Money = (int)data[0]["money"];
        GameData.StageT = (int)data[0]["stageT"];
        GameData.Stage1 = (int)data[0]["stage1"];
        GameData.Stage2 = (int)data[0]["stage2"];
        GameData.Stage3 = (int)data[0]["stage3"];
        GameData.ExStage = (int)data[0]["exStage"];
        GameData.PlayingCount = (int)data[0]["playingCount"];
        GameData.LifeHour = (int)data[0]["lifeHour"];
        GameData.LifeMin = (int)data[0]["lifeMin"];
        GameData.LifeSec = (int)data[0]["lifeSec"];
    }
}
