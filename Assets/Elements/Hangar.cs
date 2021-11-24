using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangar : MonoBehaviour
{
    [SerializeField] private BilleManager manager;
    [SerializeField] private GameObject billePrefab;
    [SerializeField] private Vector3 launchOffset;
    [SerializeField] private float maxVelocity = 32;
    public Vector3 LaunchPosition => transform.position + launchOffset;

    private void Start()
    {
        if (billePrefab == null)
            throw new System.Exception("Il faut le prefab de bille !");
    }

    public void LaunchBall(float velocity)
    {
        if (manager.CurrentBille != null)
            return;
        manager.CurrentBille = Instantiate(billePrefab, LaunchPosition, Quaternion.identity).GetComponent<Bille>();
        Rigidbody billebody = manager.CurrentBille.GetComponent<Rigidbody>();
        billebody.AddForce(Vector3.forward * velocity, ForceMode.VelocityChange);
    }

    public void InputLaunchBall(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
            LaunchBall(maxVelocity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(LaunchPosition, .5f);
    }
}
