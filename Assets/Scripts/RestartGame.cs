using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    SceneFade fadeScript;

    // Start is called before the first frame update
    void Start()
    {
        fadeScript = GetComponent<SceneFade>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (SceneManager.GetActiveScene().name != "Menu")
            {
                string sceneName = SceneManager.GetActiveScene().name;
                StartCoroutine(fadeScript.fadeScene(sceneName));
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (SceneManager.GetActiveScene().name != "Menu")
            {
                StartCoroutine(fadeScript.fadeScene("Menu"));
            }
        }
    }

    public void levelSelect()
    {
        string sceneName = gameObject.name;
        StartCoroutine(fadeScript.fadeScene(sceneName));
    }
}
