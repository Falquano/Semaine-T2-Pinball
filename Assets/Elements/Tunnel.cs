using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
    [SerializeField] private Tunnel otherEnd;
    [SerializeField] private Transform billeTarget;
    [SerializeField] private Vector3 ejectDirection;
    [SerializeField] private float ejectStrength = 16f;

    private Collider trigger;
    private Animator animator;
    private Bille attachedBille;

    private void Start()
    {
        trigger = GetComponent<Collider>();

        if (otherEnd == null)
            throw new System.Exception("Il faut un autre bout au tunnel !");

        animator = GetComponent<Animator>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, otherEnd.transform.position);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, ejectDirection * ejectStrength);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bille"))
            return;

        trigger.enabled = false;
        otherEnd.trigger.enabled = false;

        attachedBille = other.GetComponent<Bille>();

        attachedBille.AttachForAnimation(billeTarget.transform);
        animator.SetTrigger("Ball In");
    }

    public void TeleportBall()
    {
        attachedBille.AttachForAnimation(otherEnd.billeTarget.transform);
        otherEnd.attachedBille = attachedBille;
        otherEnd.animator.SetTrigger("Ball Out");

        attachedBille = null;
        Invoke("ActivateTrigger", 1f);
    }

    public void EjectBall()
    {
        attachedBille.DetachAnimation();
        attachedBille.GetComponent<Rigidbody>().AddForce(ejectDirection * ejectStrength, ForceMode.Impulse);

        attachedBille = null;

        Invoke("ActivateTrigger", 1f);
    }

    private void ActivateTrigger()
    {
        trigger.enabled = true;
    }
}
