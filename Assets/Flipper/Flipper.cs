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

    [SerializeField] private string soundOnActivation = "Other/Flipper";

    private void FixedUpdate()
    {
        Vector3 throwDirection = Vector3.zero;
        if (collisionDetector.IsColliding)
        {
            Quaternion rotation = Quaternion.Euler(0, 2f * AnglePerFrame + transform.localEulerAngles.y, 0);
            throwDirection = rotation * Vector3.forward * 1.5f; // Vers le haut
            float velocityStrength = Mathf.Log10(collisionDetector.Bille.velocity.magnitude + 1);
            throwDirection += rotation * collisionDetector.Bille.velocity.normalized * velocityStrength * .75f; // Vers la velocité
            //throwDirection = rotation * transform.forward + rotation * collisionDetector.Bille.velocity * .75f;
            float distance = Vector3.Distance(transform.position, collisionDetector.Bille.position);
            throwDirection *= distance + .1f;
            Debug.DrawRay(collisionDetector.CollisionPosition, throwDirection * distance, Color.red, 0.1f);
        }

        if (active && frameCounter < activationFrames)
        {
            if (collisionDetector.IsColliding && collisionDetector.CanThrow)
            {
                collisionDetector.Bille.AddForce(throwDirection * throwForce, ForceMode.Impulse); // Ca c'est cool, c'est propre, mais ça ne fonctionne pas
                //collisionDetector.Bille.velocity = throwDirection * throwForce;
                collisionDetector.CanThrow = false;
                //collisionDetector.IgnoreCollisionsWithBall(.25f);
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
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/" + soundOnActivation, transform.position);
            RumbleManager.Rumble(.05f, .2f);
            active = true;
        }
        else if (context.canceled)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/" + soundOnActivation, transform.position);
            RumbleManager.Rumble(.05f, .2f);
            active = false;
        }
    }
}
