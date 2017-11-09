using UnityEngine;
using System.Collections;

public class StrongJelly : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetStatus()
    {
        JellyStatus sJelly = GetComponent<JellyStatus>();
        sJelly.SetStatus(120, 10, 5);
    }
}
