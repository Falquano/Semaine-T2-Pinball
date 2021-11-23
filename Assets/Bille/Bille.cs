using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bille : MonoBehaviour
{
    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, body.velocity, Color.yellow);
    }
}
