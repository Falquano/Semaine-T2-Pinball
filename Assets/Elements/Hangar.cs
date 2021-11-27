using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangar : MonoBehaviour
{
    private bool launching = false;
    [SerializeField] private BilleManager manager;
    [SerializeField] private GameObject billePrefab;
    [SerializeField] private Vector3 launchOffset;
    [SerializeField] private float minVelocity = 12;
    [SerializeField] private float maxVelocity = 32;
    public Vector3 LaunchPosition => transform.position + launchOffset;
    [SerializeField] private string launchSoundEffect = "Other/Eject";
    [SerializeField] private float launchTimeOffset = 1.7f;
    [SerializeField] private float launchRumbleIntensity = .6f;

    private Jauge jauge;
    [SerializeField] private float currentStrength = 0f;
    private bool holding = false;
    [SerializeField] private float maxHoldTime = 1f;

    private void Start()
    {
        jauge = GetComponentInChildren<Jauge>();
        jauge.Size = 0f;
        if (billePrefab == null)
            throw new System.Exception("Il faut le prefab de bille !");
    }

    private void Update()
    {
        if (!holding)
            return;

        currentStrength = Mathf.Clamp(currentStrength + Time.deltaTime / maxHoldTime, 0f, 1f);
        jauge.Size = currentStrength;
    }

    public void LaunchBall()
    {
        holding = false;
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
        float force = (maxVelocity - minVelocity) * currentStrength + minVelocity;
        billebody.AddForce(Vector3.forward * force, ForceMode.VelocityChange);
        currentStrength = 0f;
        jauge.Size = currentStrength;
    }

    public void InputLaunchBall(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (manager.CurrentBille != null || launching)
            return;
        if (context.performed)
            holding = true;
        else if (context.canceled)
            LaunchBall();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(LaunchPosition, .5f);
    }
}
