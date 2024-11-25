using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRoomTest : MonoBehaviour
{
    public List<GameObject> lights;
    public List<BoxCollider> triggers;

    public bool lightsOn = false;
    public bool startedCoroutine = false;

    // Update is called once per frame
    void Update()
    {
        if (lightsOn)
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(true);
            }
            foreach (BoxCollider trigger in triggers)
            {
                trigger.enabled = true;
            }
        }
        else if (!lightsOn)
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(false);
            }
            foreach (BoxCollider trigger in triggers)
            {
                trigger.enabled = false;
            }
        }

        if (!startedCoroutine)
        {
            StartCoroutine(lightsOnSequence());
            startedCoroutine = true;
        }
    }

    public IEnumerator lightsOnSequence()
    {
        lightsOn = true;
        yield return new WaitForSeconds(2);
        lightsOn = false;
        yield return new WaitForSeconds(2);
        startedCoroutine = false;
    }
}
