using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Shooter : MonoBehaviour
{
    Power firePower, bombPower, speedPower;
    
    void Start() { }

    void Update() { }

    public void powerUp(PowerUpType powerUpType)
    {
        switch (powerUpType) {
            case PowerUpType.Bomb: break;
            case PowerUpType.Gun: break;
            case PowerUpType.Speed: break;
            case PowerUpType.Laser: break;            
        }        
    }

}
