using UnityEngine;
using System.Collections;

public class BigJelly : MonoBehaviour
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
        sJelly.SetStatus(300, 20, 20);
    }
}
