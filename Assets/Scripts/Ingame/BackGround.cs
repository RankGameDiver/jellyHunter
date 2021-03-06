﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {

    private float ScrollSpeed = 0.2f;
    private static Material thisMaterial;

	// Use this for initialization
	void Start () {
        thisMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 thisOffset = thisMaterial.mainTextureOffset;
        thisOffset = new Vector2(thisMaterial.mainTextureOffset.x + Time.deltaTime * ScrollSpeed, 0);
        thisMaterial.mainTextureOffset = thisOffset;
    }
}
