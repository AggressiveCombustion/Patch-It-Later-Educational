using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour {

    public Transform key;
    public Transform ring;

    public bool hasKey = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(!hasKey)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.3f);
            ring.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.3f);
            //key.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.3f);
        }

        else
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
            ring.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
            //key.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
        }
	}
}
