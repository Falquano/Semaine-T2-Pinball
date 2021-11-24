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

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        sound = GetComponent<StudioEventEmitter>();
        Manager = GameObject.FindObjectOfType<BilleManager>();
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
}
