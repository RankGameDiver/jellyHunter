using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCount : MonoBehaviour
{
    public Text timeT;
    private int cooldown;
    private string systemTime;
    private int hour;
    private int min;
    private int sec;

    void Update()
    {
        hour = System.DateTime.Now.Hour;
        min = System.DateTime.Now.Minute;
        sec = System.DateTime.Now.Second;
        systemTime = hour.ToString() + min.ToString() + sec.ToString();
        //systemTime = string.Format("{0,-10} {1,-10:N0}", );
        timeT.text = System.DateTime.Now.TimeOfDay.ToString();
    }

    public void timeSet()
    {

    }
}
