using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangar : MonoBehaviour
{
    private bool launching = false;
    [SerializeField] private BilleManager manager;
    [SerializeField] private GameObject billePrefab;
    [SerializeField] private Vector3 launchOffset;
    [SerializeField] private float maxVelocity = 32;
    public Vector3 LaunchPosition => transform.position + launchOffset;
    [SerializeField] private string launchSoundEffect = "Other/Eject";
    [SerializeField] private float launchTimeOffset = 1.7f;
    [SerializeField] private float launchRumbleIntensity = .6f;

    private void Start()
    {
        if (billePrefab == null)
            throw new System.Exception("Il faut le prefab de bille !");
    }

    public void LaunchBall()
    {
        if (manager.CurrentBille != null || launching)
            return;
        launching = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/" + launchSoundEffect);
        RumbleManager.Rumble(launchTimeOffset, launchRumbleIntensity);
        Invoke("Eject", launchTimeOffset);
    }

    public void Eject()
    {
        launching = false;
        manager.CurrentBille = Instantiate(billePrefab, LaunchPosition, Quaternion.identity).GetComponent<Bille>();
        Rigidbody billebody = manager.CurrentBille.GetComponent<Rigidbody>();
        billebody.AddForce(Vector3.forward * maxVelocity, ForceMode.VelocityChange);
    }

    public void InputLaunchBall(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
            LaunchBall();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(LaunchPosition, .5f);
    }
}
