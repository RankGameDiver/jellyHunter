using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreText : MonoBehaviour {

    public ScoreManager score;
    Text scoreT;

	void Awake () {
        scoreT = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreT.text = ScoreManager.score.ToString();
	}

}
