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
    private int frameCounter = 0;
    private float AnglePerFrame => AngleDifference / (float)activationFrames;

    private bool active = false;

    private void FixedUpdate()
    {
        if (active && frameCounter < activationFrames)
        {
            transform.localEulerAngles = transform.localEulerAngles + Vector3.up * AnglePerFrame;
            frameCounter++;

            if (collisionDetector.IsColliding)
            {
                Quaternion rotation = Quaternion.Euler(0, 1.5f * AnglePerFrame + transform.localEulerAngles.y, 0);
                Vector3 direction = rotation * Vector3.forward;
                Debug.DrawRay(collisionDetector.CollisionPosition, direction, Color.red, 0.1f);
                collisionDetector.Bille.AddForce(direction * 2, ForceMode.VelocityChange);
            }
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
