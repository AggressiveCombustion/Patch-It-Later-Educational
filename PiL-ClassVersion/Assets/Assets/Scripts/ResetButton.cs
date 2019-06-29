using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour {

    public float resetValue = 0.0f;
    public float increaseTime = 2.0f;

    public Transform ring;
    public Transform rButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        ring.GetComponent<Image>().fillAmount = resetValue;

        if (Input.GetKey(KeyCode.R))
        {
            rButton.GetComponent<Image>().color = new Color(255, 255, 255, 1.0f);
            resetValue += increaseTime * Time.deltaTime;
        }
        else if(resetValue > 0)
        {
            rButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.3f);
            resetValue -= increaseTime * Time.deltaTime;
        }
        else
        {
            rButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.3f);
        }

        resetValue = Mathf.Clamp(resetValue, 0.0f, 1.0f);

        if (resetValue >= 1)
        {
            GameManager.instance.PlaySoundPlace();
            GameManager.instance.RestartLevel();
        }
		
	}

    public void HandleReset()
    {

    }
}
