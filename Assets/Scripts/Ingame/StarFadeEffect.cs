using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFadeEffect : MonoBehaviour {

    public UnityEngine.UI.Image StarImg;
    public int starNum;
    private float alpha = 0;
    private float time=0;

    // Use this for initialization
    void Start () {
        StarImg.color = new Color(1, 1, 1, alpha);
	}

    // Update is called once per frame 
    void Update()
    {
        time += 0.02f;
        if (time>(float)starNum*2&&ScoreManager.score>20000*starNum)
        {
            if(alpha<1)
            {
                alpha += 0.02f;
            }
        }
        StarImg.color = new Color(1, 1, 1, alpha);
    }
}
