using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public void IsClicked()
    {
        SceneManager.LoadScene("StageSelect");
        GameData.StageT = 4;
        if (GameData.Stage1 == 0)
            GameData.Stage1 = 1;
    }
}
