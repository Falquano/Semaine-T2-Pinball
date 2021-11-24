using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Bille : MonoBehaviour
{
    public BilleManager Manager { get; set; }
    private Rigidbody body;
    private StudioEventEmitter sound;
    private ConstantForce force;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        sound = GetComponent<StudioEventEmitter>();
        Manager = GameObject.FindObjectOfType<BilleManager>();
        force = GetComponent<ConstantForce>();
    }

    private void OnDestroy()
    {
        Manager.CurrentBille = null;
    }

    private void Update()
    {
        sound.SetParameter("Vitesse", body.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, body.velocity, Color.yellow);
    }

    public void DeactivatePhysics()
    {
        force.enabled = false;
        body.isKinematic = false;
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
    }

    public void ActivatePhysics()
    {
        force.enabled = true;
        body.isKinematic = false;
    }

    public void AttachForAnimation(Transform target)
    {
        transform.parent = target;
        transform.localPosition = Vector3.zero;
        DeactivatePhysics();
    }

    public void DetachAnimation()
    {
        transform.parent = null;
        ActivatePhysics();
    }
}
