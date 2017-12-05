using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{

    public Canvas canvas;
    private GameObject[] window;
    private GameObject[] button;
    private GameObject shop;
    private GameObject stageSelect;
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

        shop = GameObject.Find("Shop");
        stageSelect = GameObject.Find("StageSelect");

        window[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-1750, -138);
        window[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-1194, -135);
        window[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-439, -124);

        button[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-1534, -134);
        button[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-1002, -134);
        button[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-104, -134);

        shop.GetComponent<RectTransform>().anchoredPosition = new Vector2(488, -96);
        stageSelect.GetComponent<RectTransform>().anchoredPosition = new Vector2(-255, 384);
    }
}
