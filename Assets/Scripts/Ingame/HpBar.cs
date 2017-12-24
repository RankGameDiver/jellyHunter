using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {

    public Slider HP;
    public GameObject HeadUpPos;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        HP.value = CatStatus.GetHealth() / 200;
        HP.transform.position = HeadUpPos.transform.position;
	}
}
