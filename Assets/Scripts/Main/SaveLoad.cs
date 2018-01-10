using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    [SerializeField]
    private List<Data> S_Data = new List<Data>();
    private List<Data> L_Data = new List<Data>();

    void Start()
    {
        Load();
        Debug.Log("LoadData");
    }

    public void Save()
    {
        S_Data.Add(new Data(GameData.Money));
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
        GameData._Money = (int)data[0]["money"];
    }
}
