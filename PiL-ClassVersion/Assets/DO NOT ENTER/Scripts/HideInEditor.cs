using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HideInEditor : MonoBehaviour
{

    public GameObject[] hide;

    void Update()
    {
        foreach (GameObject go in hide)
        {

            if (Application.isPlaying)
                go.SetActive(true);
            else
                go.SetActive(false);
        }
    }
}