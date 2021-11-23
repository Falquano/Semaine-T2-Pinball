using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flipper : MonoBehaviour
{
    [SerializeField] private FlipperCollisionDetector collisionDetector;

    [SerializeField] private float restingAngle = 0;
    [SerializeField] private float activeAngle = -30;
    private float AngleDifference => activeAngle - restingAngle;

    [SerializeField] private int activationFrames = 5;

    [SerializeField] private float throwForce = 6f;
    private int frameCounter = 0;
    private float AnglePerFrame => AngleDifference / (float)activationFrames;

    private bool active = false;

    private void FixedUpdate()
    {
        Vector3 throwDirection = Vector3.zero;
        if (collisionDetector.IsColliding)
        {
            Quaternion rotation = Quaternion.Euler(0, 8f * AnglePerFrame + transform.localEulerAngles.y, 0); // Pourquoi 8 ? Je ne sais pas.
            throwDirection = rotation * Vector3.forward + // Vers le haut
                rotation * collisionDetector.Bille.velocity * .75f; // Vers la velocité
            //throwDirection = rotation * transform.forward + rotation * collisionDetector.Bille.velocity * .75f;
            float distance = Vector3.Distance(transform.position, collisionDetector.Bille.position);
            throwDirection *= distance + .5f;
            Debug.DrawRay(collisionDetector.CollisionPosition, throwDirection * distance, Color.red, 0.1f);
            Debug.DrawRay(collisionDetector.Bille.position, collisionDetector.Bille.velocity, Color.blue, 0.1f);
        }

        if (active && frameCounter < activationFrames)
        {
            if (collisionDetector.IsColliding && collisionDetector.CanThrow)
            {
                collisionDetector.Bille.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
                collisionDetector.CanThrow = false;
            }
            transform.localEulerAngles = transform.localEulerAngles + Vector3.up * AnglePerFrame;
            frameCounter++;
        } else if (!active && frameCounter > 0)
        {
            transform.localEulerAngles = transform.localEulerAngles - Vector3.up * AnglePerFrame;
            frameCounter--;
        }
    }

    public void BumperInput(InputAction.CallbackContext context)
    {
        if (context.performed)
            active = true;
        else if (context.canceled)
            active = false;
    }

}
