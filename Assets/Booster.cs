using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private Vector3 force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bille"))
            other.attachedRigidbody.AddForce(force, ForceMode.VelocityChange);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + force);
    }
}
