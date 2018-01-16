using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {

    private Slider HP;
    public CatStatus catStatus;

    void Start()
    {
        HP = GetComponent<Slider>();
    }

	void Update ()
    {
        HP.value = catStatus.GetHealth() / catStatus.GetMaxHP();
	}
}
