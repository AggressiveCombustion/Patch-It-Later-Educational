using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverInfo : MonoBehaviour {

    public Transform[] turnOn;
    public Transform[] turnOff;

    bool done = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(!done && GameObject.FindObjectOfType<Mover>() != null)
        {
            DoTheThing();
            done = true;
        }
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
