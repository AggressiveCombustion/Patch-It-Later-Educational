using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

    public PanelFade panel;

	// Use this for initialization
	void Start () {
        //panel.FadeIn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        GameManager.instance.AddTimer(2, GoToLevelSelect);
        GameManager.instance.PlaySoundPlace();
        panel.FadeOut();
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsMenu()
    {
        GameManager.instance.AddTimer(2, GoToOptions);
        GameManager.instance.PlaySoundPlace();
        panel.FadeOut();
    }

    public void GoToOptions()
    {
        SceneManager.LoadScene("options");
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
