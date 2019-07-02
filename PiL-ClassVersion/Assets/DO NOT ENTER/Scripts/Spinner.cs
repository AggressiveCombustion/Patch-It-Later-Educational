using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

    public float rotSpeed = 50.0f;
    public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        target.transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
	}
}
