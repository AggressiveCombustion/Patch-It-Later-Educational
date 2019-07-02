using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustMusic : MonoBehaviour
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
            counters[i].GetComponent<Image>().enabled = GameManager.instance.bgm >= i + 1;
        }
    }

    public void IncreaseValue()
    {
        GameManager.instance.bgm += 1;

        if (GameManager.instance.bgm > 4)
        {
            GameManager.instance.bgm = 4;
        }
    }

    public void DecreaseValue()
    {
        GameManager.instance.bgm -= 1;
        if (GameManager.instance.bgm < 0)
        {
            GameManager.instance.bgm = 0;
        }
    }
}
