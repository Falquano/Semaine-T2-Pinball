using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BilleFollower : MonoBehaviour
{
    [SerializeField] private BilleManager billeManager;
    [SerializeField] private Transform activePosition;
    [SerializeField] private Transform restingPosition;
    private Transform currentTarget;
    [SerializeField] private Vector3 offsetFromObject;
    [SerializeField] private float distanceFromObject;
    private Vector3 currentVelocity;
    [SerializeField] private float smoothTime = .5f;

    private void Start()
    {
        billeManager.OnBilleChange.AddListener(ChangeBille);
        currentTarget = restingPosition;
    }

    void Update()
    {
        Follow(currentTarget);
    }

    void Follow(Transform target)
    {
        if (target == null)
        {
            currentTarget = restingPosition;
            target = currentTarget;
        }

        Vector3 targetPosition = currentTarget.position;
        targetPosition.x = 0;
        targetPosition += offsetFromObject.normalized * distanceFromObject;
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    private void ChangeBille(Bille newBille)
    {
        if (newBille == null)
        {
            currentTarget = restingPosition;
            activePosition = null;
        }
        else
        {
            activePosition = newBille.GetComponent<Transform>();
            currentTarget = activePosition.transform;
        }
    }
}
