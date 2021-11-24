using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryLights : MonoBehaviour
{
    [SerializeField] private GameObject[] lights;
    [SerializeField] private float lightsDuration = .2f;

    void Start()
    {
        DeactivateLights();
    }

    public void ActivateLights()
    {
        foreach (GameObject light in lights)
            light.SetActive(true);
        Invoke("DeactivateLights", lightsDuration);
    }

    protected void DeactivateLights()
    {
        foreach (GameObject light in lights)
            light.SetActive(false);
    }
}
