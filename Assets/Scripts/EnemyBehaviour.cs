using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    Rigidbody rb;

    public bool inView = false;
    public bool spotted = false;

    public Slider visionCooldownSlider;
    public GameObject spottedExclamation;

    public SceneFade fadeScript;

    [SerializeField] float sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inView && !spotted)
        {
            if (!visionCooldownSlider.gameObject.activeSelf)
            {
                visionCooldownSlider.gameObject.SetActive(true);
            }
            visionIncrease();
        }
        else if (!inView && !spotted)
        {
            visionDecrease();
        }
        else if (spotted)
        {
            spottedExclamation.SetActive(true);

            //spotted
            string currentScene = SceneManager.GetActiveScene().name;
            StartCoroutine(fadeScript.fadeScene(currentScene));
        }
    }

    public void visionIncrease()
    {
        if (visionCooldownSlider.value < visionCooldownSlider.maxValue)
        {
            visionCooldownSlider.value += sensitivity * Time.deltaTime;
        }
        else
        {
            visionCooldownSlider.value = visionCooldownSlider.maxValue;
            visionCooldownSlider.gameObject.SetActive(false);

            spotted = true;
        }
    }
    public void visionDecrease()
    {
        if (visionCooldownSlider.value > visionCooldownSlider.minValue)
        {
            visionCooldownSlider.value -= (sensitivity/2) * Time.deltaTime;
        }
        else
        {
            visionCooldownSlider.value = visionCooldownSlider.minValue;
            if (visionCooldownSlider.gameObject.activeSelf)
            {
                visionCooldownSlider.gameObject.SetActive(false);
            }
        }
    }
}
