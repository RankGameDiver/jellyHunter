using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreText : MonoBehaviour {

    Text scoreT;
    Text moneyT;

	void Awake () {
        scoreT = GetComponent<Text>();
        moneyT = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        scoreT.text = ScoreManager.score.ToString();
        moneyT.text = ScoreManager.money.ToString();
    }
}
