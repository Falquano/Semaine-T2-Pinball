using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bille : MonoBehaviour
{
    private Rigidbody body;
    private StudioEventEmitter sound;
    
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        sound = GetComponent<StudioEventEmitter>();
    }

    private void Update()
    {
        sound.SetParameter("Vitesse", body.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, body.velocity, Color.yellow);
    }

	private void OnCollisionEnter(Collision collision)
	{
        FMODUnity.RuntimeManager.PlayOneShot("event:/Obstacles/Mur");
    }
}
