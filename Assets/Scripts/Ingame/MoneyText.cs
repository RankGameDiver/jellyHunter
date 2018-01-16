using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    Text moneyT;

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
