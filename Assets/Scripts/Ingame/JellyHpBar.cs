using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JellyHpBar : MonoBehaviour {

    public Slider HP;
    public GameObject HeadUpPos;
    private int maxHP;
    private int JKind;

	// Use this for initialization
	void Start () {
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
	void Update ()
    {
        HP.value = JellyStatus.JHealth / maxHP;
        HP.transform.position = HeadUpPos.transform.position;
    }
}
