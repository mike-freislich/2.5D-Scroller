using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum QualityLevel { VeryLow, Low, Medium, High, VeryHigh, Ultra }
enum GameLayer {
    Default, TransparentFX, IgnoreRayCast, Water, UI, PlayerBullets, Platform, Player, Enemy
}

public class application : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        QualitySettings.SetQualityLevel((int)QualityLevel.VeryHigh);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
