using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Carousel : MonoBehaviour
{
	private float anglePerSecond = 0f;
	[SerializeField] private float maxAngle = 1440f;
	[SerializeField] private float timeToReachMaxSpeed = 2f;
	private float angleModificatorPerSecond => maxAngle / timeToReachMaxSpeed;
	private float angleAxis;

	public void FixedUpdate()
	{
		anglePerSecond = Mathf.Clamp(anglePerSecond + angleAxis * angleModificatorPerSecond * Time.fixedDeltaTime, -maxAngle, maxAngle);

		transform.Rotate(Vector3.up * anglePerSecond * Time.deltaTime);
	}

	public void InputSpeedAxis(CallbackContext context)
	{
		angleAxis = context.ReadValue<float>();
	}
}
