﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
