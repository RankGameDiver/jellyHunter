using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void Shop()
    {
        Debug.Log("Shop!");
    }

	public void StageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }
}