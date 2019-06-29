using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustGameSpeed : MonoBehaviour {

    public Transform[] counters;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        for(int i = 0; i < counters.Length; i++)
        {
            counters[i].GetComponent<Image>().enabled = GameManager.instance.speedMult >= i + 1;
        }
	}

    public void IncreaseValue()
    {
        GameManager.instance.speedMult += 1;

        if (GameManager.instance.speedMult > 4)
        {
            GameManager.instance.speedMult = 4;
        }
    }

    public void DecreaseValue()
    {
        GameManager.instance.speedMult -= 1;
        if(GameManager.instance.speedMult < 1)
        {
            GameManager.instance.speedMult = 1;
        }
    }
}
