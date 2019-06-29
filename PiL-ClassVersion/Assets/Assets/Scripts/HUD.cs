using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    

    public int world = 1;
    public int level = 1;

    public Text[] levelNameText;
    public Image[] icons;
    public Text[] points;

    public int index = 0;

    public ObjectManager obm;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        index = obm.objectIndex;
        
        // "1-1" at the top of the screen
        foreach(Text t in levelNameText)
        {
            t.text =  (GameManager.instance.levelName).ToUpper();
        }

        // count of each item
        for(int i = 0; i < points.Length; i++)
        {
            if(obm.points.Length > i)
                points[i].text = obm.points[i] + "";
            //points[i].color = Color.red;
            /*if (obm.points[i] <= 0)
            {
                points[i].color = Color.red;
            }

            else if (index == i)
            {
                points[i].color = Color.yellow;
            }

            else
            {
                points[i].color = Color.white;
            }*/
        }

        for(int i = 0; i < icons.Length; i++)
        {
            if (obm.points.Length <= i)
                break;

            icons[i].sprite = obm.sprites[i];
            icons[i].preserveAspect = true;

            Color newColor;

            if (obm.points[i] <= 0)
            {
                newColor = (Color.white);
                newColor.a = 0.3f;
                icons[i].color = newColor;
            }

            else if (index == i)
            {
                newColor = Color.yellow;
                icons[i].color = newColor;
            }

            else
            {
                icons[i].color = Color.white;
            }
        }
	}
}
