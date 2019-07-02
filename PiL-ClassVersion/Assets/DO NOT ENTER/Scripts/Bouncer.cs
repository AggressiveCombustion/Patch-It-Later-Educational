using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour {

    bool bounced = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoBounceAnimation()
    {
        if(bounced)
        {
            return;
        }

        GetComponent<Animator>().SetTrigger("bounce");
        bounced = true;
    }

    public void ResetBouncer()
    {
        bounced = false;
    }
}
