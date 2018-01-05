using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JellyHpBar : MonoBehaviour {

    public Slider HP;
    public GameObject HeadUpPos;
    public GameObject Jelly;
    private int maxHP;
    private int JKind;

    private void Awake()
    {
        if (Jelly.activeInHierarchy) { this.gameObject.SetActive(true); }
    }

    // Use this for initialization
    void Start ()
    {
        if (Jelly.activeInHierarchy) { this.gameObject.SetActive(true); }
        switch (JellyStatus.JKind)
        {
            case 0:
                maxHP = 50;
                break;
            case 1:
                maxHP = 120;
                break;
            case 2:
                maxHP = 250;
                break;
        }
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Jelly.activeInHierarchy) { this.gameObject.SetActive(true); }
        HP.value = JellyStatus.JHealth / maxHP;
        HP.transform.position = HeadUpPos.transform.position;
    }
}
