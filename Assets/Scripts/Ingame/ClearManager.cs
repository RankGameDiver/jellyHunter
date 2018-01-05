using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Money();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Back()
    {
        SceneManager.LoadScene("Main");
    }

    private void Money()
    {

    }
}
