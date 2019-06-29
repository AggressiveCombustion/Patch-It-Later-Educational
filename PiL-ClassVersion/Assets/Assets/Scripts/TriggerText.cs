using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerText : MonoBehaviour
{

    public Transform[] turnOn;
    public Transform[] turnOff;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoTheThing()
    {
        foreach (Transform t in turnOn)
        {
            if (t != null)
                t.gameObject.SetActive(true);
            //t.GetComponent<Canvas>().enabled = false;
        }

        foreach (Transform t in turnOff)
        {
            if (t != null)
                t.gameObject.SetActive(false);
            //t.GetComponent<Canvas>().enabled = false;
        }

        Destroy(gameObject);
    }
}