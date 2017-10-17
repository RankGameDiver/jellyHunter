using UnityEngine;
using System.Collections;

public class NormalJelly : JellyStatus
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
        sJelly.health = 50;
        sJelly.damage = 8;
        sJelly.defend = 5;
    }

}
