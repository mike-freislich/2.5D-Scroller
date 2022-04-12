using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorAnimation : MonoBehaviour
{
    float rotorSpeed;
    Transform mainRotor, tailRotor;
    Vector3 tailRotorAngle = new Vector3(1,0,0);
    void Start()
    {    
        rotorSpeed = 1000;
        mainRotor = transform.Find("enemyChopper1/Main Rotor");        
        tailRotor = transform.Find("enemyChopper1/Tail Rotor");
    }

    void Update()
    {
        mainRotor.Rotate(Vector3.forward * Time.deltaTime * rotorSpeed);        
        tailRotor.Rotate(Vector3.left * Time.deltaTime * rotorSpeed);
    }    
}
