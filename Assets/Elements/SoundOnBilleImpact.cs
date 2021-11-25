using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnBilleImpact : MonoBehaviour
{
    [SerializeField] private string soundPath = "Obstacles/Mur";
    [SerializeField] private float rumbleFrequency = .5f;
    [SerializeField] private float rumbleDuration = .1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bille"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/" + soundPath, collision.contacts[0].point);
            RumbleManager.Rumble(rumbleDuration, rumbleFrequency);
        }
    }
}
