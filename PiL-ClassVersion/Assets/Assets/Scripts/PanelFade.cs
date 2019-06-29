using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFade : MonoBehaviour {

    Color bgColor;
    public float currentAlpha = 1;

    bool increaseAlpha = false;
    bool decreaseAlpha = false;

    public float fadeSpeed = 1.0f;

    bool started = false;

	// Use this for initialization
	void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {
        if(!started)
        {
            GameManager.instance.AddTimer(1f, FadeIn);
            started = true;
        }
        bgColor = GameManager.instance.backgroundColor;
        bgColor.a = currentAlpha;
        GetComponent<Image>().color = bgColor;

        if(increaseAlpha)
        {
            currentAlpha += fadeSpeed * Time.deltaTime;
        }

        if (decreaseAlpha)
        {
            currentAlpha -= fadeSpeed * Time.deltaTime;
        }

        if(currentAlpha >= 1)
        {
            increaseAlpha = false;
        }
        if(currentAlpha <= 0)
        {
            decreaseAlpha = false;
        }
    }

    public void FadeIn()
    {
        decreaseAlpha = true;
        increaseAlpha = false;
    }

    public void FadeOut()
    {
        increaseAlpha = true;
        decreaseAlpha = false;
    }
}
