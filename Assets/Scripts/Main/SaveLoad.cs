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
        //Load();
    }

    public void Save()
    {
        JsonData charData = JsonMapper.ToJson(S_Data);
        File.WriteAllText(Application.dataPath + "/Resources/SaveData.json", charData.ToString());
        //S_Data.Add(new Data(GameData._Money));
    }

    public void Load()
    {
        string L_sData = File.ReadAllText(Application.dataPath + "/Resources/SaveData.json");
        JsonData charData = JsonMapper.ToObject(L_sData);
        GetData(charData);
    }

    public void GetData(JsonData data)
    {
        GameData._Money = (int)data[0]["money"];
    }
}
