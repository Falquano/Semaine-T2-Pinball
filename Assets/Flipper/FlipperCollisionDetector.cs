using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperCollisionDetector : MonoBehaviour
{
    public bool IsColliding { get; private set; }
    public Vector3 CollisionPosition { get; private set; }
    public Vector3 ThrowAngle { get; private set; }
    public Rigidbody Bille { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        IsColliding = true;
        Bille = collision.rigidbody;
    }

    private void OnCollisionStay(Collision collision)
    {
        CollisionPosition = collision.GetContact(0).point;
    }

    private void OnCollisionExit(Collision collision)
    {
        IsColliding = false;
        Bille = null;
    }
}
