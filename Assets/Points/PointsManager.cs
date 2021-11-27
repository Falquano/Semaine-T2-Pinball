using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    private int points = 0;
    public int Points { get => points; set => SetPoints(value); }
    public int HighScore { get; private set; }

    [SerializeField] private int mediumPoint = 150;
    [SerializeField] private int highPoint = 500;

    [SerializeField] private string lowPointSoundPath = "Score/Gain/ScoreEasy";
    [SerializeField] private string mediumPointSoundPath = "Score/Gain/ScoreMedium";
    [SerializeField] private string highPointSoundPath = "Score/Gain/ScoreHigh";

    [SerializeField] private Text pointUIText;
    [SerializeField] private Text highscoreUIText;

    private void Start()
    {
        LoadHighScore();
        highscoreUIText.text = "High score " + HighScore;
        pointUIText.text = "Points " + points;
    }

    private void OnDestroy()
    {
        SaveHighScore();
    }

    private void SetPoints(int value)
    {
        int increase = value - points;
        points = value;
        pointUIText.text = "Points " + points;

        PointFX(increase);

        if (Points > HighScore)
        {
            HighScore = Points;
            highscoreUIText.text = "High score " + HighScore;
        }
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
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("highscore", HighScore);
    }

    private void LoadHighScore()
    {
        HighScore = PlayerPrefs.GetInt("highscore");
    }
}
