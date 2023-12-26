using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private int score;
    private int highScore;

    // To demonstrate Unity Events
    public UnityEvent OnScoreUpdated;
    public UnityEvent OnHighScoreUpdated;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        OnHighScoreUpdated?.Invoke();
        GameManager.GetInstance().OnGameStart += OnGameStart;
    }

    public int GetScore()
    {
        return score;
    }
    public int GetHighScore()
    {
        return highScore;
    }

    public void IncrementScore()
    {
        score++;
        OnScoreUpdated?.Invoke();

        if(score > highScore)
        {
            highScore = score;
            OnHighScoreUpdated?.Invoke();
        }
    }

    public void SetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void OnGameStart()
    {
        score = 0;
    }
}
