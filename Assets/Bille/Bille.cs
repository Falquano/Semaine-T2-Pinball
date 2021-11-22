using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bille : MonoBehaviour
{
    [SerializeField] private Vector3 gravity = -Vector3.forward;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        body.AddForce(gravity, ForceMode.Acceleration);
    }
}
