using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    [SerializeField]
    private List<Data> S_Data = new List<Data>();

    void Start()
    {
        Application.runInBackground = true;
        Load();
    }

    public void Save()
    {
        S_Data.Clear();
        S_Data.Add(new Data(GameData.Money, GameData.StageT, GameData.Stage1, GameData.Stage2, GameData.Stage3,
                            GameData.PlayingCount, GameData.LifeHour, GameData.LifeMin, GameData.LifeSec));
        JsonData charData = JsonMapper.ToJson(S_Data);
        File.WriteAllText(Application.dataPath + "/Data/SaveData.json", charData.ToString());
    }

    public void Load()
    {
        string L_sData = File.ReadAllText(Application.dataPath + "/Data/SaveData.json");
        JsonData charData = JsonMapper.ToObject(L_sData);
        GetData(charData);
    }

    public void GetData(JsonData data)
    {
        GameData.Money = (int)data[0]["money"];
        GameData.StageT = (int)data[0]["stageT"];
        GameData.Stage1 = (int)data[0]["stage1"];
        GameData.Stage2 = (int)data[0]["stage2"];
        GameData.Stage3 = (int)data[0]["stage3"];
        GameData.PlayingCount = (int)data[0]["playingCount"];
        GameData.LifeHour = (int)data[0]["lifeHour"];
        GameData.LifeMin = (int)data[0]["lifeMin"];
        GameData.LifeSec = (int)data[0]["lifeSec"];
    }
}
