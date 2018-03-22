using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{

    public Canvas canvas;
    private GameObject[] window;
    private GameObject[] button;
    public int scale;

    void Awake()
    {
        window = new GameObject[3];
        button = new GameObject[3];
        SetUI();
    }

    private void SetUI()
    {
        window[0] = GameObject.Find("diaWindow");
        window[1] = GameObject.Find("jellyWindow");
        window[2] = GameObject.Find("lifeWindow");

        button[0] = GameObject.Find("diaButton");
        button[1] = GameObject.Find("jellyButton");
        button[2] = GameObject.Find("lifeButton");
    }

    public void StageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
