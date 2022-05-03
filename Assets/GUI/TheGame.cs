using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheGame : MonoBehaviour
{
    public int highScore;
    public int score;
    public int lives;
    public int stage;

    public Text highScoreText;
    public Text scoreText;
    public Text livesText;
    public Text deathText;
    public Text bossTimerText;
    public GameObject progressBarObject;   
    
    ProgressBar progressBar;

    private static TheGame instance;
    public static TheGame Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.Find("Player").GetComponent<TheGame>();                
            return instance;
        }
    }

    void Start()
    {
        progressBar = progressBarObject.GetComponent<ProgressBar>();
        lives = 3;
        HideBossHealth();
        HideBossTimer();        
    }

    void Update() { }

    public void PlayerDied(string reason)
    {
        ChangeLives(-1);
        deathText.text = $"Death by {reason}";
        StartCoroutine(MyTimer.Start(2.0f, false, () => { deathText.text = ""; }));
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"SCORE: {score.ToString("D9")}";
        if (score > highScore)
            SetHighScore(score);
    }

    public void SetHighScore(int newHighScore)
    {
        highScore = newHighScore;
        highScoreText.text = $"HIGH: {highScore.ToString("D9")}";
    }

    void ChangeLives(int byHowMuch)
    {
        lives += byHowMuch;
        lives = Mathf.Max(0, lives);
        livesText.text = $"LIVES: {lives}";
    }

    public void DisplayBossTimer(int secondsRemaining)
    {
        bossTimerText.text = $"{secondsRemaining}";
    }

    public void HideBossTimer()
    {
        bossTimerText.text = "";        
    }

    public void DisplayBossHealth(float percent)
    {       
        if (progressBar != null)
        { 
            progressBar.Show();
            progressBar.SetValue(percent);
        }
    }

    public void HideBossHealth()
    {
        if (progressBar != null)
            progressBar.Hide();         
    }

}
