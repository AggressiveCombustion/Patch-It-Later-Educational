using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DontMoveInEditor : MonoBehaviour {

    public Vector3 offset = new Vector3(0, -0.16f, 0);

	// Use this for initialization
	void OnEnable () {
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.parent != null)
        transform.position = transform.parent.position + offset;
	}
}
