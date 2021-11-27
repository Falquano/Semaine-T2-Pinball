using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Carousel : MonoBehaviour
{
	private float anglePerSecond = 0f;
	private float rotationSign => Mathf.Sign(anglePerSecond);
	[SerializeField] private float maxSpeed = 1440f;
	[SerializeField] private float timeToReachMaxSpeed = 2f;
	private float angleModificatorPerSecond => maxSpeed / timeToReachMaxSpeed;
	private float angleAxis;
	[SerializeField] private StudioEventEmitter soundEmitter;

	[SerializeField] private float strength = 3f;

	private Rigidbody bille;

    public void FixedUpdate()
	{
		anglePerSecond = Mathf.Clamp(anglePerSecond + angleAxis * angleModificatorPerSecond * Time.fixedDeltaTime, -maxSpeed, maxSpeed);
		soundEmitter.SetParameter("CarrouselleVitesse", anglePerSecond / maxSpeed);

		transform.Rotate(Vector3.up * anglePerSecond * Time.fixedDeltaTime);

		if (bille == null)
			return;

		Vector3 force = bille.position - transform.position;
		force = Quaternion.Euler(0, 90 * rotationSign, 0) * force;
		force *= Mathf.Abs(anglePerSecond) / maxSpeed * strength;

		bille.AddForce(force, ForceMode.Acceleration);

		Debug.DrawRay(bille.position, force, Color.cyan);
	}

    public void InputSpeedAxis(CallbackContext context)
	{
		angleAxis = context.ReadValue<float>();
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.CompareTag("Bille"))
		{
			bille = other.attachedRigidbody;
		}
    }

    private void OnTriggerExit(Collider other)
    {
		if (other.CompareTag("Bille"))
			bille = null;
    }
}
