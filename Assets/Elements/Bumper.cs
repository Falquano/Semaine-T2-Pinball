using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] private float strength = 4f;
    [SerializeField] private GameObject bumpLights;
    [SerializeField] private float bumpLightsDuration = .2f;

	private void Start()
	{
        DeactivateLights();
	}

	private void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = collision.gameObject.transform.position - transform.position;
        collision.rigidbody.AddForce(direction.normalized * strength, ForceMode.Impulse);

        BumpFX();
    }

    private void BumpFX()
	{
        bumpLights.SetActive(true);
        Invoke("DeactivateLights", bumpLightsDuration);
	}

    private void DeactivateLights()
	{
        bumpLights.SetActive(false);
	}
}
