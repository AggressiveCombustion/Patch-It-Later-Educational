using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{

    public float resetValue = 0.0f;
    public float increaseTime = 2.0f;

    public Transform ring;
    public Transform bButton;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        ring.GetComponent<Image>().fillAmount = resetValue;

        if (resetValue >= 1)
            return;

        if (Input.GetKey(KeyCode.Backspace))
        {
            bButton.GetComponent<Image>().color = new Color(255, 255, 255, 1.0f);
            resetValue += increaseTime * Time.deltaTime;
        }
        else if (resetValue > 0)
        {
            bButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.3f);
            resetValue -= increaseTime * Time.deltaTime;
        }
        else
        {
            bButton.GetComponent<Image>().color = new Color(255, 255, 255, 0.3f);
        }

        resetValue = Mathf.Clamp(resetValue, 0.0f, 1.0f);

        if (resetValue >= 1)
            HandleBack();

    }

    public void HandleBack()
    {
        GameManager.instance.PlaySoundPlace();
        FindObjectOfType<PanelFade>().FadeOut();
        // level select only exists in original game, not edu version!
        /*if(SceneManager.GetActiveScene().name == "LevelSelect")
        {
            GameManager.instance.AddTimer(2, GameManager.instance.ReturnToMainMenu);
        }
        else
            GameManager.instance.AddTimer(2, GameManager.instance.ReturnToLevelSelect);*/
        GameManager.instance.AddTimer(2, GameManager.instance.ReturnToMainMenu);
    }

    public void ResetFull()
    {
        resetValue = 1;
        HandleBack();
    }
}
