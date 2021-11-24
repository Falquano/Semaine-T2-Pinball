using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimant : MonoBehaviour
{
	private BilleManager manager;
    public bool MagnetOn { get; set; } = false;
    [SerializeField] private Rigidbody bille;
	[SerializeField] private float strength = 2f;
	[SerializeField] private float maxRange = 6f;
	[SerializeField] private float minRange = 2f;

	private void Start()
	{
		if (manager == null)
			manager = GameObject.FindObjectOfType<BilleManager>();
		if (manager == null)
			throw new System.Exception("Il faut un BilleManager dans la scène !");

		manager.OnBilleChange.AddListener(SetBille);
	}

	public void SetBille(Bille newBille)
	{
		if (newBille == null)
		{
			bille = null;
			return;
		}
		bille = newBille.GetComponent<Rigidbody>();
	}

	void FixedUpdate()
    {
		if (!MagnetOn || bille == null)
			return;
		Vector3 direction = (transform.position - bille.position).normalized;
		float distanceStrength = Vector3.Distance(transform.position, bille.position) - minRange;
		distanceStrength = 1 - (distanceStrength - minRange) / (maxRange - minRange);
		distanceStrength = Mathf.Clamp(distanceStrength, 0f, 1f);

		bille.AddForce(direction * strength * distanceStrength, ForceMode.Force);
		Debug.DrawRay(bille.position, direction * strength, Color.blue);
    }

	public void InputMagnet(UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (context.performed)
			MagnetOn = true;
		else if (context.canceled)
			MagnetOn = false;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, maxRange);
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(transform.position, minRange);
	}
}
