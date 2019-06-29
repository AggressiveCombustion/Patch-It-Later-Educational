﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoalReached()
    {
        anim.SetBool("goal", true);
    }

    public void EndLevel()
    {
        Destroy(gameObject);
    }
}
