using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    private int points = 0;
    public int Points { get => points; set => SetPoints(value); }
    public int HighScore { get; set; } = 1000;

    [SerializeField] private int mediumPoint = 150;
    [SerializeField] private int highPoint = 500;

    [SerializeField] private string lowPointSoundPath = "Score/Gain/ScoreEasy";
    [SerializeField] private string mediumPointSoundPath = "Score/Gain/ScoreMedium";
    [SerializeField] private string highPointSoundPath = "Score/Gain/ScoreHigh";
    [SerializeField] private string highScoreSoundPath = "Score/HighScore";

    [SerializeField] private Text pointUIText;

    private void SetPoints(int value)
    {
        points = value;
        pointUIText.text = value.ToString();

        PointFX(value);
        if (Points > HighScore)
            HighScore = Points;
    }

    private void PointFX(int pointChange)
    {
        if (pointChange > highPoint)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/" + highPointSoundPath);
        }
        else if (pointChange > mediumPoint)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/" + mediumPointSoundPath);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/" + lowPointSoundPath);
            Debug.Log("Points");
        }
    }
}
