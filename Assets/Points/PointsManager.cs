using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    private int points = 0;
    public int Points { get => points; set => SetPoints(value); }

    [SerializeField] private Text pointUIText;

    private void SetPoints(int value)
    {
        points = value;
        pointUIText.text = value.ToString();
    }
}
