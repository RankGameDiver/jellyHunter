using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public Canvas canvas;
    private GameObject saveButton;
    private GameObject loadButton;

    void Awake()
    {
        SetButton();
    }

    /*transform.Find("오브젝트 이름"); // 자식이나 부모 관계 안에서만 찾음
    GameObject.Find("오브젝트 이름"); // Hierarchy안에서 다 찾음
    GameObject.FindWithTag("태그 이름"); // 해당하는 태그에 속하는 오브젝트 반환. 여러 개일 경우 그 중에 랜덤으로 반환*/

    private void SetButton()
    {
        saveButton = GameObject.Find("Save");
        loadButton = GameObject.Find("Load");
        RectTransform saveRect = saveButton.GetComponent<RectTransform>();
        saveRect.anchoredPosition = new Vector2(200,-100);
        RectTransform loadRect = loadButton.GetComponent<RectTransform>();
        loadRect.anchoredPosition = new Vector2(-200, -100);
    }
}
