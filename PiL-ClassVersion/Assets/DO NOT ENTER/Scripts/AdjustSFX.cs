using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustSFX : MonoBehaviour
{

    public Transform[] counters;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < counters.Length; i++)
        {
            counters[i].GetComponent<Image>().enabled = GameManager.instance.sfx >= i + 1;
        }
    }

    public void IncreaseValue()
    {
        GameManager.instance.sfx += 1;

        if (GameManager.instance.sfx > 4)
        {
            GameManager.instance.sfx = 4;
        }
    }

    public void DecreaseValue()
    {
        GameManager.instance.sfx -= 1;
        if (GameManager.instance.sfx < 0)
        {
            GameManager.instance.sfx = 0;
        }
    }
}
