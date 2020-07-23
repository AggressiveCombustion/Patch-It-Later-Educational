using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {

    public PanelFade panel;
    bool musicOn = false;
    public Text musicText;

	// Use this for initialization
	void Start () {
        //panel.FadeIn();
        
    }
	
	// Update is called once per frame
	void Update () {
        musicText.text = "MUSIC: " + (musicOn ? "ON" : "OFF");
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

    public void ToggleMusic()
    {
        musicOn = !musicOn;

        GameManager.instance.bgm = musicOn ? 3 : 0;
    }
}
