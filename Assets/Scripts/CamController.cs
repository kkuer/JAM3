using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CamController : MonoBehaviour
{
    public List<GameObject> cameras;
    public GameObject currentCam;
    public int currentCamIndex;

    public GameObject thermalCam;
    public bool thermalToggle = false;

    public GameObject UI_StandardDetails;
    public GameObject UI_ThermalDetails;

    public AudioSource SFX_ThermalOn;
    public AudioSource SFX_ThermalOff;

    public TMP_Text camIndicator;
    public TMP_Text camName;

    void Start()
    {
        currentCam = cameras[0];
        currentCamIndex = 0;
    }

    void Update()
    {
        camIndicator.text = "CAM " + (currentCamIndex + 1);
        camName.text = cameras[currentCamIndex].name;

        //switch cameras
        if (Input.GetKeyDown(KeyCode.RightArrow))    //next camera
        {
            nextCam(currentCam);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))   //prev camera
        {
            prevCam(currentCam);
        }

        //toggle thermal layer
        if (Input.GetKeyDown(KeyCode.F))
        {
            //play visual effect

            //toggle on
            if (!thermalToggle)
            {
                thermalCam.SetActive(true);
                thermalToggle = true;

                SFX_ThermalOn.Play();
                UI_ThermalDetails.SetActive(true);
                UI_StandardDetails.SetActive(false);
            }
            //toggle off
            else
            {
                thermalCam.SetActive(false);
                thermalToggle = false;

                SFX_ThermalOff.Play();
                UI_StandardDetails.SetActive(true);
                UI_ThermalDetails.SetActive(false);
            }
        }
    }

    public void nextCam(GameObject newCam)
    {
        //check position in cameras list and set new cam to next in line
        if ((currentCamIndex + 1) != cameras.Count)
        {
            //move to next cam in line
            newCam = cameras[currentCamIndex + 1];

            //set camera index variable to match next in line
            currentCamIndex += 1;
        }
        //if index is at last in list then move to start
        else
        {
            newCam = cameras[0];
            currentCamIndex = 0;
        }

        //hide other cameras
        foreach (GameObject cam in cameras)
        {
            if (cam != newCam)
            {
                cam.SetActive(false);
            }
            else
            {
                cam.SetActive(true);
            }
        }
    }

    public void prevCam(GameObject newCam)
    {
        //check position in cameras list and set new cam to previous in line
        if (currentCamIndex != 0)
        {
            //move to previous cam in line
            newCam = cameras[currentCamIndex - 1];

            //set camera index variable to match previous in line
            currentCamIndex -= 1;
        }
        //if index is at first in list then move to end
        else
        {
            newCam = cameras[cameras.Count - 1];
            currentCamIndex = cameras.Count - 1;
        }

        //hide other cameras
        foreach (GameObject cam in cameras)
        {
            if (cam != newCam)
            {
                cam.SetActive(false);
            }
            else
            {
                cam.SetActive(true);
            }
        }
    }
}