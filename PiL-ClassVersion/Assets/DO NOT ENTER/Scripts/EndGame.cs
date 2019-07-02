using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Backspace) ||
            Input.GetKeyDown(KeyCode.R) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetMouseButtonDown(0))
        {
            HandleBack();
        }
	}

    public void HandleBack()
    {
        GameManager.instance.PlaySoundPlace();
        GameObject.Find("PanelFade").GetComponent<PanelFade>().FadeOut();
        GameManager.instance.AddTimer(2, GameManager.instance.ReturnToMainMenu);
    }
}
