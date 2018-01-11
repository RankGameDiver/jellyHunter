using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {

    private Slider HP;

    void Start()
    {
        HP = GetComponent<Slider>();
    }

	void Update ()
    {
        HP.value = CatStatus.GetHealth() / 200;
	}
}
