using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    //public static MusicManager instance;

    //AudioSource source;


	// Use this for initialization
	void Start () {

        /*if (instance != null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);*/

        GameObject.DontDestroyOnLoad(gameObject);
        /*source = GetComponent<AudioSource>();
        source.enabled = true;
        source.Play();*/
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<AudioSource>().volume = GameManager.instance.bgm * 0.25f;
	}
}
