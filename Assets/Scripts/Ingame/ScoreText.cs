using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    Text scoreT;

	void Awake ()
    {
        scoreT = GetComponent<Text>();
    }

	void Update ()
    {
        scoreT.text = ScoreManager.score.ToString();
    }
}
