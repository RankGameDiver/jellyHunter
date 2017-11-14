using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

    public GameObject Me, Next;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.touchCount>0)
        {
            Debug.Log("HEY!");
            OnMouseDown();
        }
	}
    
    void OnMouseDown()
    {
        Next.SetActive(true);
        Me.SetActive(false);

        if(!Next)
        {
            SceneManager.LoadScene("StageSelect");
        }
    }
}
