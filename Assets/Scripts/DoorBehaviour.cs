using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehaviour : MonoBehaviour
{
    public SceneFade fadeScript;

    public bool inRange = false;

    public GameObject popup;
    public string sceneName;

    void Update()
    {
        //detect for input and in range
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            StartCoroutine(fadeScript.fadeScene(sceneName));
        }

        if (inRange)
        {
            inRange = true;
            popup.SetActive(true);
        }
        else if (!inRange)
        {
            inRange = false;
            popup.SetActive(false);
        }
    }
}
