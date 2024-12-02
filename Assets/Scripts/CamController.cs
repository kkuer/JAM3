using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.Rendering;

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
    public AudioSource SFX_CamSwitch;

    public TMP_Text camIndicator;
    public TMP_Text camName;

    public GameObject camFlash;

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
            SFX_CamSwitch.Play();
            StartCoroutine(flash());
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))   //prev camera
        {
            prevCam(currentCam);
            SFX_CamSwitch.Play();
            StartCoroutine(flash());
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
            CinemachineVirtualCamera camComponent = cam.GetComponent<CinemachineVirtualCamera>();

            if (cam != newCam)
            {
                cam.transform.Find("CCTV").gameObject.SetActive(true);
                camComponent.enabled = false;
            }
            else
            {
                cam.transform.Find("CCTV").gameObject.SetActive(false);
                camComponent.enabled = true;
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
            CinemachineVirtualCamera camComponent = cam.GetComponent<CinemachineVirtualCamera>();

            if (cam != newCam)
            {
                cam.transform.Find("CCTV").gameObject.SetActive(true);
                camComponent.enabled = false;
            }
            else
            {
                cam.transform.Find("CCTV").gameObject.SetActive(false);
                camComponent.enabled = true;
            }
        }
    }

    IEnumerator flash()
    {
        camFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        camFlash.SetActive(false);
    }
}