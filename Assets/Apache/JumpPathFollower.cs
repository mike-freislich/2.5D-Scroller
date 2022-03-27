using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class JumpPathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed;
    
    float distanceTravelled;
    

    void Start()
    {
    }
    
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;    
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
    }
}
