using UnityEngine;
using System.Collections;

public class StrongJelly : JellyStatus
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
        sJelly.health = 120;
        sJelly.damage = 15;
        sJelly.defend = 10;
    }
}
