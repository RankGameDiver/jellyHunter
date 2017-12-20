using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    //public List<PlayerCharacter> S_CharList = new List<PlayerCharacter>();
    //public List<PlayerInfo> playerInfo = new List<PlayerInfo>();

    //void Start()
    //{
    //    S_CharList.Add(new PlayerCharacter(0, 1, "Buck", 10, 5, 4, 3, 2, 1));
    //    playerInfo.Add(new PlayerInfo());
    //}

    //public void Save()
    //{
    //    JsonData charData = JsonMapper.ToJson(S_CharList);
    //    File.WriteAllText(Application.dataPath + "/Resource/PlayerChar.json", charData.ToString());
    //    //Debug.Log(S_CharList);

    //    JsonData playerData = JsonMapper.ToJson(playerInfo);
    //    File.WriteAllText(Application.dataPath + "/Resource/PlayerInfo.json", playerData.ToString());
    //}

    //public void Load()
    //{
    //    string L_PlayerCharData = File.ReadAllText(Application.dataPath + "/Resource/PlayerChar.json");
    //    Debug.Log(L_PlayerCharData);
    //    JsonData charData = JsonMapper.ToObject(L_PlayerCharData);
    //    GetData(charData);

    //    string L_PlayerData = File.ReadAllText(Application.dataPath + "/Resource/PlayerInfo.json");
    //    Debug.Log(L_PlayerData);
    //    JsonData playerData = JsonMapper.ToObject(L_PlayerData);
    //    GetData(playerData);
    //}

    //private void GetData(JsonData name)
    //{
    //    for (int i = 0; i < name.Count; i++)
    //    {
    //        Debug.Log(name[i]["ID"]);
    //        string tempID = name[i]["ID"].ToString();

    //        for (int j = 0; j < S_CharList.Count; j++)
    //        {
    //            if (tempID == S_CharList[j].ID.ToString())
    //            {
    //                playerInfo[0].CharList.Remove(S_CharList[j]);
    //                playerInfo[0].CharList.Add(S_CharList[j]);
                    
    //            }
    //        }
    //    }
    //    for (int i = 0; i < playerInfo[0].CharList.Count; i++)
    //    {
    //        Debug.Log("불러온 데이터 = " + playerInfo[0].CharList[i].Name);
    //    }
    //}
}
