using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIText : MonoBehaviour {

    public Text moneyT;

    void Awake()
    {
        moneyT = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyT.text = ScoreManager.money.ToString();
    }
}
