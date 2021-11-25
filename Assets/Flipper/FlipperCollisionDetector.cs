using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperCollisionDetector : MonoBehaviour
{
    [SerializeField] private BilleManager billeManager;
    private Collider flipperCollider;
    public bool IsColliding { get; private set; }
    public Vector3 CollisionPosition { get; private set; }
    public Vector3 ThrowAngle { get; private set; }
    public Rigidbody Bille { get; set; }
    public bool CanThrow { get; set; }

    [SerializeField] private Vector3 pivot;

    private Collider billeCollider;

    private void Start()
    {
        flipperCollider = GetComponent<Collider>();
        billeManager.OnBilleChange.AddListener(ChangeBille);
    }

    public void IgnoreCollisionsWithBall(float duration)
    {
        billeCollider = Bille.GetComponent<Collider>();
        Physics.IgnoreCollision(flipperCollider, billeCollider, true);
        Invoke("StopIgnoringCollisionsWithBall", duration);
    }

    private void StopIgnoringCollisionsWithBall()
    {
        if (billeCollider == null)
            throw new System.Exception("Something went wrong...");

        Physics.IgnoreCollision(flipperCollider, billeCollider, false);
        billeCollider = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsColliding = true;
        Bille = collision.rigidbody;
        CanThrow = true;
        CollisionPosition = collision.GetContact(0).point;
    }

    private void OnCollisionStay(Collision collision)
    {
        CollisionPosition = collision.GetContact(0).point;
    }

    private void OnCollisionExit(Collision collision)
    {
        IsColliding = false;
        Bille = null;
        CanThrow = true;
    }

    public void ChangeBille(Bille newBille)
    {
        IsColliding = false;
        Bille = null;
        CanThrow = true;
    }
}
