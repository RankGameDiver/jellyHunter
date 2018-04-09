using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private int itemKind;
    private RectTransform shopPos { get { return GetComponent<RectTransform>(); } }
    public RectTransform mask;
    public GameObject[] Item;
    private int price = 100;

    public void AttackUp()
    {
        if (!GameData.attackItem && GameData.Money > price)
        {
            GameData.attackItem = true;
            Item[0].SetActive(false);
            GameData.Money -= price;
        }
        else if (GameData.attackItem)
        {
            GameData.attackItem = false;
            Item[0].SetActive(true);
            GameData.Money += price;
        }
        else { }
    }

    public void DefendUp()
    {
        if (!GameData.defendItem && GameData.Money > price)
        {
            GameData.defendItem = true;
            Item[1].SetActive(false);
            GameData.Money -= price;
        }
        else if (GameData.defendItem)
        {
            GameData.defendItem = false;
            Item[1].SetActive(true);
            GameData.Money += price;
        }
        else { }
    }

    public void HealUp()
    {
        if (!GameData.healItem && GameData.Money > price)
        {
            GameData.healItem = true;
            Item[2].SetActive(false);
            GameData.Money -= price;
        }
        else if (GameData.healItem)
        {
            GameData.healItem = false;
            Item[2].SetActive(true);
            GameData.Money += price;
        }
        else { }
    }

    public void MoneyUp()
    {
        if (!GameData.moneyItem && GameData.Money > price)
        {
            GameData.moneyItem = true;
            Item[3].SetActive(false);
            GameData.Money -= price;
        }
        else if (GameData.moneyItem)
        {
            GameData.moneyItem = false;
            Item[3].SetActive(true);
            GameData.Money += price;
        }
        else { }
    }

    public void HealthUp()
    {
        if (!GameData.healthItem && GameData.Money > price)
        {
            GameData.healthItem = true;
            Item[4].SetActive(false);
            GameData.Money -= 500;
        }
        else if (GameData.healthItem)
        {
            GameData.healthItem = false;
            Item[4].SetActive(true);
            GameData.Money += 500;
        }
        else { }
    }
}
