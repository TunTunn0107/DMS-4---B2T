using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAuto : MonoBehaviour
{
    public float delay = 1.0f;
    public List<GameObject> lightObjectsOn; // List to hold multiple on lights
    public List<GameObject> lightObjectsOff; // List to hold multiple off lights

    private bool isToggling = false; // Flag to prevent coroutine from being called multiple times

    void Update()
    {
        if (!isToggling)
        {
            StartCoroutine(Lightauto());
        }
    }

    IEnumerator Lightauto()
    {
        isToggling = true; // Set flag to true to indicate the coroutine is running

        // Turn all 'On' lights on and 'Off' lights off
        foreach (GameObject lightOn in lightObjectsOn)
        {
            lightOn.SetActive(true);
        }
        foreach (GameObject lightOff in lightObjectsOff)
        {
            lightOff.SetActive(false);
        }

        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Turn all 'On' lights off and 'Off' lights on
        foreach (GameObject lightOn in lightObjectsOn)
        {
            lightOn.SetActive(false);
        }
        foreach (GameObject lightOff in lightObjectsOff)
        {
            lightOff.SetActive(true);
        }

        yield return new WaitForSeconds(delay);

        isToggling = false; // Reset flag to false to allow coroutine to be called again
    }
}
