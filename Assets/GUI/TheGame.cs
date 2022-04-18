using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheGame : MonoBehaviour
{
    public int highScore;
    public int score;
    public int bossHealth;
    public int bossTimer;
    public int lives;
    public Text highScoreText;
    public Text scoreText;
    public Text livesText;
    public Text deathText;

    public static TheGame Instance { get{
        return GameObject.Find("Player").GetComponent<TheGame>();
    }}

    void Start()
    {
        lives = 3;
    }

    void Update()
    {
        
    }

    public void PlayerDied(string reason)
    {   
        ChangeLives(-1);
        deathText.text = $"Death by {reason}";
        StartCoroutine(MyTimer.Start(2.0f, false, () => { deathText.text = ""; }));
    }

    public void AddScore(int scoreToAdd) {
        score += scoreToAdd;
        scoreText.text = $"SCORE: {score.ToString("D9")}";
        if (score > highScore)
            SetHighScore(score);
    }

    public void SetHighScore(int newHighScore) {
        highScore = newHighScore;
        highScoreText.text = $"HIGH: {highScore.ToString("D9")}";
    }

    void ChangeLives(int byHowMuch) {
        lives += byHowMuch;
        lives = Mathf.Max(0, lives);
        livesText.text = $"LIVES: {lives}";
    }

    
}
