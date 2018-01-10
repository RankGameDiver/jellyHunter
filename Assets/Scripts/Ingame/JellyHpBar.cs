using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JellyHpBar : MonoBehaviour {

    public Slider HP;
    public GameObject HeadUpPos;
    public GameObject Jelly;
    private int maxHP;
    private int JKind;

    //private void Awake()
    //{
    //    if (Jelly.activeInHierarchy) { this.gameObject.SetActive(true); Debug.Log("HEY!"); }
    //    else { this.gameObject.SetActive(false); Debug.Log("Bye!"); }
    //}

    // Use this for initialization
    void Start ()
    {
        if (Jelly.activeInHierarchy)
        {
            gameObject.SetActive(true);
            Debug.Log("HEY!");
        }
        else
        {
            gameObject.SetActive(false);
            Debug.Log("Bye!");
        }
        switch (JellyStatus.JKind)
        {
            case 0:
                maxHP = 50;
                break;
            case 1:
                maxHP = 120;
                break;
            case 2:
                maxHP = 250;
                break;
        }
		
	}

    // Update is called once per frame
    void Update()
    {
        HP.value = JellyStatus.JHealth / maxHP;
        HP.transform.position = HeadUpPos.transform.position;
    }
}
