#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ShouldNotAdd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EditorApplication.hierarchyChanged += ExampleCallback;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void ExampleCallback()
    {
        /*Object[] all = Resources.FindObjectsOfTypeAll(typeof(Object));
        Debug.Log("There are " + all.Length + " objects at the moment.");*/

        if(FindObjectOfType<Mover>() != null || FindObjectOfType<Spinner>() != null || FindObjectOfType<Stopper>() != null)
        {
            Debug.Log("IMPORTANT! vMovers, hMovers, spinners, and stoppers are MODIFIERS! They do nothing on their own and should be added to AllObjects in ObjectManager");
        }
    }
}
#endif