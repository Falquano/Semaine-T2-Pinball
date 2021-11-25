using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    private Vector3 offset;

    private Vector3 velocity;

    [SerializeField] private float moveTime = 2f;
    private float currentTime;

    private void Start()
    {
        offset = transform.position;
    }

    private void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        //transform.position = Vector3.SmoothDamp(transform.position, offset + endPosition, ref velocity, moveTime);
        transform.position = Vector3.Lerp(transform.position, offset + endPosition, currentTime / moveTime);

        if (currentTime >= moveTime * 1.5f)
            Loop();
    }

    private void Loop()
    {
        Vector3 tmp = startPosition;
        startPosition = endPosition;
        endPosition = tmp;
        currentTime = 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(offset + startPosition, Vector3.one);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(offset + endPosition, Vector3.one);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position + startPosition, Vector3.one);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + endPosition, Vector3.one);
        }
    }
}
