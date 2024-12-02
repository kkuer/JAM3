using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCams : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject levelCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && levelCam.activeInHierarchy == false)
        {
            levelCam.SetActive(true);
            menuCam.SetActive(false);
        }
    }
}
