using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckForFail : MonoBehaviour {

    public bool outside = true;

    public string itemNeeded;

    public bool inBox = false;

    public Transform[] turnOn;
    public Transform[] turnOff;
    public Transform elseOn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        // if the item we need exists
        if(GameObject.Find(itemNeeded) != null)
        {
            /*// is it not in the box?
            if (!inBox && outside)
            {
                Debug.Log("NOT IN THE BOX");
                DoTheThing();
            }

            // is it inside the box
            else if (inBox && !outside)
            {
                DoTheThing();
            }

            else
            {
                DoTheOtherThing();
            }*/
            if(Vector3.Distance(transform.position, GameObject.Find(itemNeeded).transform.position) < 2.5f)
            {
                DoTheOtherThing();
            }
            else
            {
                DoTheThing();
            }

        }
	}

    /*public void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTERED");
        if(other.name.Contains(itemNeeded))
        {
            Debug.Log("IN THE BOX");
            inBox = true;
        }
    }*/

    public void DoTheThing()
    {
        foreach(Transform t in turnOn)
        {
            if(t != null)
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

    public void DoTheOtherThing()
    {
        elseOn.gameObject.SetActive(true);

        foreach (Transform t in turnOff)
        {
            if (t != null && t != elseOn)
                t.gameObject.SetActive(false);
            //t.GetComponent<Canvas>().enabled = false;
        }

        Destroy(gameObject);
    }
}
