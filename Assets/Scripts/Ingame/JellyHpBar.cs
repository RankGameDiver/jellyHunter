using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JellyHpBar : MonoBehaviour
{
    public JellyStatus jelly;
    private Slider HP;
    private RectTransform pos;
    private int maxHP;

    public void SetHpBar ()
    {
        HP = GetComponent<Slider>();
        pos = GetComponent<RectTransform>();
        switch (jelly.jellyKind)
        {
            case 0:
                maxHP = 50;
                pos.anchoredPosition = new Vector2(0.2f, 1.2f);
                break;
            case 1:
                maxHP = 120;
                pos.anchoredPosition = new Vector2(0.2f, 1.2f);
                break;
            case 2:
                maxHP = 250;
                pos.anchoredPosition = new Vector2(0, 0.7f);
                break;
            case 3:
                maxHP = 75;
                pos.anchoredPosition = new Vector2(0.2f, 1.2f);
                break;
        }
		
	}

    void Update()
    {
        HP.value = jelly.GetHealth() / maxHP;
    }
}
