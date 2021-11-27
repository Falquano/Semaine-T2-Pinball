using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private int maxLives;
    private int currentLives;

    [SerializeField] private Text livesUIText;

    private PointsManager pointsManager;

    private void Start()
    {
        currentLives = maxLives;
        livesUIText.text = currentLives + " Lives";
        pointsManager = GetComponent<PointsManager>();
    }

    public void LoseOneLife()
    {
        currentLives--;
        livesUIText.text = currentLives + " Lives";
        if (currentLives <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
