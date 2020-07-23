using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public float gravity = 5.0f;

    List<Timer> timers = new List<Timer>();

    // options
    public bool cameraShakeOn = true;
    public Color backgroundColor;

    public int sfx = 2;
    public int bgm = 2;
    public float speed = 1.0f;
    public int speedMult = 1;

    public string levelName = "";

    public AudioClip[] clips;
    public AudioSource source;

	// Use this for initialization
	void Start () {

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        source = GetComponent<AudioSource>();
        GameObject.DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {

        speed = 1 * speedMult;
        GetComponent<AudioSource>().volume = GameManager.instance.sfx * 0.25f;

        levelName = SceneManager.GetActiveScene().name;

        foreach (Timer t in timers)
        {
            t.Update();
        }

        for (int i = 0; i < timers.Count; i++)
        {
            if (timers[i].done)
            {
                timers.RemoveAt(i);
            }
        }
	}

    public void AddTimer(float d, TimerEvent e)
    {
        Timer t = new Timer(d, e);
        timers.Add(t);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShakeCamera()
    {
        if(!cameraShakeOn)
            return;

        Debug.Log("SHAKE");

        Camera.main.GetComponent<Animator>().SetTrigger("shake");
        //GameObject.Find("Main Camera").GetComponent<Animator>().SetTrigger("shake");
    }

    public void CompleteLevel()
    {
        GameObject.Find("PanelFade").GetComponent<PanelFade>().FadeOut();
        AddTimer(2.5f, GoToNextLevel);
    }

    void GoToNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            ReturnToMainMenu();
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //RestartLevel();
    }

    public void ReturnToLevelSelect()
    {
        SceneManager.LoadScene(2);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlaySoundPlace()
    {
        source.clip = clips[0];
        source.Play();
    }

    public void PlaySoundSwish()
    {
        source.clip = clips[1];
        source.Play();
    }
    public void PlaySoundCrack()
    {
        source.clip = clips[2];
        source.Play();
    }

    public void PlaySoundBurn()
    {
        source.clip = clips[3];
        source.Play();
    }

    public void PlaySoundChop()
    {
        source.clip = clips[4];
        source.Play();
    }
}

public delegate void TimerEvent();

public class Timer
{
    public float duration;
    public float elapsed;
    public TimerEvent onTimer;
    public bool done = false;

    public Timer(float d, TimerEvent e)
    {
        duration = d;
        onTimer = e;
    }

    public void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed > duration && !done)
        {
            done = true;
            onTimer();
        }
    }
}
