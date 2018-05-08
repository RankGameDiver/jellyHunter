using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    Text moneyT;

    void Awake()
    {
        moneyT = GetComponent<Text>();
    }

    void Update()
    {
        moneyT.text = ScoreManager.money.ToString();
    }
}
