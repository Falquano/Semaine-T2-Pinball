using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jauge : MonoBehaviour
{
    [SerializeField] private float maxSize = 1f;
    private float size = 1f;
    public float Size { get => size; set => SetSize(value); }
    private float ActualSize => Size * maxSize;

    private void SetSize(float value)
    {
        size = value;
        transform.localScale = new Vector3(transform.localScale.x, ActualSize, transform.localScale.z);
    }
}
