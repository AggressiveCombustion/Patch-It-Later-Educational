using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {

    public float rotSpeed = 1.0f;
    public bool on = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(on)
            transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
	}
}
