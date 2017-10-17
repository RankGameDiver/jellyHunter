using UnityEngine;
using System.Collections;

public class BigJelly : JellyStatus
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetStat()
    {
        JellyStatus sJelly = GetComponent<JellyStatus>();
        sJelly.health = 300;
        sJelly.damage = 25;
        sJelly.defend = 20;
    }
}
