using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointGiver : MonoBehaviour
{
    [SerializeField] protected PointsManager pointsManager;
    [SerializeField] protected int points = 100;
    public virtual int Points => points;

    [SerializeField] protected UnityEvent OnPoints;


    protected void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bille"))
            return;

        pointsManager.Points += Points;
        OnPoints.Invoke();
    }
}
