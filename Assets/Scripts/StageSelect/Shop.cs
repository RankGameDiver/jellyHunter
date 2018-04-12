using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private int itemKind;
    private RectTransform shopPos { get { return GetComponent<RectTransform>(); } }
    public RectTransform mask;
    public GameObject[] Item;
    private int price = 400;

    public GameObject msgBox; // 게임머니가 부족할 시에 표시되는 메시지 창
    private float alpha;

    void Update()
    {
        if (msgBox.GetComponent<Image>().color.a > 0)
        { FadeOutMsg(); }
        else { }
    }

    public void SetAlpha(float _alpha)
    {
        Color msgColor = msgBox.GetComponent<Image>().color;
        Color textColor = msgBox.transform.GetChild(0).GetComponent<Text>().color;
        msgBox.GetComponent<Image>().color = new Color(msgColor.r, msgColor.g, msgColor.b, _alpha);
        msgBox.transform.GetChild(0).GetComponent<Text>().color =
            new Color(textColor.r, textColor.g, textColor.b, _alpha);
    }

    public void FadeOutMsg()
    {
        Color msgColor = msgBox.GetComponent<Image>().color;
        Color textColor = msgBox.transform.GetChild(0).GetComponent<Text>().color;
        alpha = msgBox.GetComponent<Image>().color.a;
        alpha -= 0.02f;
        msgBox.GetComponent<Image>().color = new Color(msgColor.r, msgColor.g, msgColor.b, alpha);
        msgBox.transform.GetChild(0).GetComponent<Text>().color = 
            new Color(textColor.r, textColor.g, textColor.b, alpha);
    }

    ////////////////////////////// 아이템 구매 ////////////////////////////////

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
        else { SetAlpha(1); }
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
        else { SetAlpha(1); }
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
        else { SetAlpha(1); }
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
        else { SetAlpha(1); }
    }

    public void HealthUp()
    {
        if (!GameData.healthItem && GameData.Money > price)
        {
            GameData.healthItem = true;
            Item[4].SetActive(false);
            GameData.Money -= price;
        }
        else if (GameData.healthItem)
        {
            GameData.healthItem = false;
            Item[4].SetActive(true);
            GameData.Money += price;
        }
        else { SetAlpha(1); }
    }
}
