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
        Load();
    }

    public void Save()
    {
        S_Data.Clear();
        S_Data.Add(new Data(GameData.Money, GameData.StageT, GameData.Stage1, GameData.Stage2, GameData.Stage3));
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
    }
}
