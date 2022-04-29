using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState { idle, active, dying, dead };

public class Boss : MonoBehaviour
{
    public float timeRemaining;
    public BossState bossState;
    public delegate void OnEnteredCameraViewDelegate();
    public OnEnteredCameraViewDelegate OnEnteredCameraView;
    public delegate void OnMoveDelegate();
    public OnMoveDelegate OnMove;
    
    TheGame theGame;
    Enemy enemy;

    void Start()
    {
        bossState = BossState.idle;
        theGame = TheGame.Instance;
        enemy = gameObject.GetComponent<Enemy>();                    
    }

    void Update()
    {
        UpdateCountdownTimer();

        switch (bossState)
        {
            case BossState.idle: if (ScreenUtility.isOnCamera(transform)) ActivateBoss(); break;
            case BossState.active: OnMove(); break;
        }
    }

    void UpdateCountdownTimer()
    {
        if (bossState == BossState.active)
        {
            timeRemaining = Mathf.Max(timeRemaining - Time.deltaTime, 0);
            if (timeRemaining == 0)
                TimeIsUp();
            else
            {
                theGame.DisplayBossTimer((int)timeRemaining);
                DisplayBossHealth();
            }
        }
    }

    void DisplayBossHealth()
    {
        float maxHealth = (float)enemy.GetMaxHealth;
        float currentHealth = (float)enemy.health;
        theGame.DisplayBossHealth(currentHealth / maxHealth);
    }

    void TimeIsUp()
    {
        bossState = BossState.dying;   
        theGame.HideBossTimer();
        // Lose a life and return to last checkpoint
    }

    void ActivateBoss()
    {
        bossState = BossState.active;        
        OnEnteredCameraView();
    }
}
