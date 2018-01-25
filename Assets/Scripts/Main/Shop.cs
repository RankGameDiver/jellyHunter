using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private int itemKind;
    private RectTransform shopPos;
    public RectTransform mask;
    public GameObject[] Item;



    void Start()
    {
        shopPos = GetComponent<RectTransform>();
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
        if (GameData.attackUp)
        {
            GameData.attackUp = false;
            Item[0].SetActive(true);
        }
        else
        {
            GameData.attackUp = true;
            Item[0].SetActive(false);
        }
    }

    public void DefendUp()
    {
        if (GameData.defendUp)
        {
            GameData.defendUp = false;
            Item[1].SetActive(true);
        }
        else
        {
            GameData.defendUp = true;
            Item[1].SetActive(false);
        }
    }

    public void HealUp()
    {
        if (GameData.healUp)
        {
            GameData.healUp = false;
            Item[2].SetActive(true);
        }
        else
        {
            GameData.healUp = true;
            Item[2].SetActive(false);
        }
    }

    public void MoneyUp()
    {
        if (GameData.moneyUp)
        {
            GameData.moneyUp = false;
            Item[3].SetActive(true);
        }
        else
        {
            GameData.moneyUp = true;
            Item[3].SetActive(false);
        }
    }

    public void HealthUp()
    {
        if (GameData.hpUp)
        {
            GameData.hpUp = false;
            Item[4].SetActive(true);
        }
        else
        {
            GameData.hpUp = true;
            Item[4].SetActive(false);
        }
    }
}
