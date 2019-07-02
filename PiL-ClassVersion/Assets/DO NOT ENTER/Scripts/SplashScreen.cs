using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

    float elapsed = 0;
    bool done = false;

	// Use this for initialization
	void Start () {
        //GameManager.instance.AddTimer(2, GameObject.Find("PanelFade").GetComponent<PanelFade>().FadeOut);
        //GameManager.instance.AddTimer(2, GameManager.instance.CompleteLevel);
		
	}
	
	// Update is called once per frame
	void Update () {

        elapsed += 1;

        if(elapsed > 2 && !done)
        {
            done = true;
            GameManager.instance.AddTimer(3, GameManager.instance.CompleteLevel);
        }
	}
}
