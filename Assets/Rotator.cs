using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	[SerializeField] private float maxSpeed = 1220;
	[SerializeField] private float timeToReachMaxSpeed = 1f;
	private float anglePerSecond = 0f;
    private float angleModificatorPerSecond => maxSpeed / timeToReachMaxSpeed;

    public bool Active { get; set; }

    private void FixedUpdate()
    {
        anglePerSecond = Mathf.Clamp(anglePerSecond + (Active ? 1f : -1f) * angleModificatorPerSecond * Time.fixedDeltaTime, 0, maxSpeed);
        transform.Rotate(Vector3.up * anglePerSecond * Time.fixedDeltaTime);
    }
}
