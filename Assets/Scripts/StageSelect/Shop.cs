using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private int itemKind;
    private RectTransform shopPos { get { return GetComponent<RectTransform>(); } }
    public RectTransform mask;
    public GameObject[] Item;

    void Start()
    {
        //shopPos.anchoredPosition = new Vector3(-25, -200, 100);
        for (int i = 0; i < 2; i++)
        {
            AttackUp();
            DefendUp();
            HealUp();
            MoneyUp();
            HealthUp();
        }
    }

    public void ShopResetPos()
    {
        //mask.anchoredPosition = new Vector3(1175, -100, 0);
    }

    public void AttackUp()
    {
        if (GameData.attackItem)
        {
            GameData.attackItem = false;
            Item[0].SetActive(true);
        }
        else
        {
            GameData.attackItem = true;
            Item[0].SetActive(false);
        }
    }

    public void DefendUp()
    {
        if (GameData.defendItem)
        {
            GameData.defendItem = false;
            Item[1].SetActive(true);
        }
        else
        {
            GameData.defendItem = true;
            Item[1].SetActive(false);
        }
    }

    public void HealUp()
    {
        if (GameData.healItem)
        {
            GameData.healItem = false;
            Item[2].SetActive(true);
        }
        else
        {
            GameData.healItem = true;
            Item[2].SetActive(false);
        }
    }

    public void MoneyUp()
    {
        if (GameData.moneyItem)
        {
            GameData.moneyItem = false;
            Item[3].SetActive(true);
        }
        else
        {
            GameData.moneyItem = true;
            Item[3].SetActive(false);
        }
    }

    public void HealthUp()
    {
        if (GameData.healthItem)
        {
            GameData.healthItem = false;
            Item[4].SetActive(true);
        }
        else
        {
            GameData.healthItem = true;
            Item[4].SetActive(false);
        }
    }
}
