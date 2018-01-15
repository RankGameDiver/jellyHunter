using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private int itemKind;
    private RectTransform shopPos;

    void Start()
    {
        shopPos = GetComponent<RectTransform>();
        shopPos.anchoredPosition = new Vector3(0, 0, 100);
    }

    public void Buy()
    {
        switch (itemKind)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }
}
