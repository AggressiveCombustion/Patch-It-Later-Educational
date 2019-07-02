using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour {

    public bool drop = false;
    public float fallSpeed = 10.0f;
    public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(drop)
        {
            transform.Translate(Vector3.up * -1 * fallSpeed * Time.deltaTime);
        }

        if(transform.position.y < -100)
        {
            Destroy(gameObject);
        }

	}

    public void StartDrop()
    {
        /*if (drop)
            return;*/

        drop = true;
        
    }
}
