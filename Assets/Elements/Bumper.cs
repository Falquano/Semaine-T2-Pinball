using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] private float strength = 4f;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = collision.gameObject.transform.position - transform.position;
        collision.rigidbody.AddForce(direction.normalized * strength, ForceMode.Impulse);
        Debug.Log("BUMP !");
    }
}
