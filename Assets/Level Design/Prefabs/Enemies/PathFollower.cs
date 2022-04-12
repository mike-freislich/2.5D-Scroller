using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

// Moves along a path at constant speed.
// Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    
    float distanceTravelled;

    void Start()
    {
        if (pathCreator != null)                    
            pathCreator.pathUpdated += OnPathChanged;        
    }

    void Update()
    {
        if (pathCreator != null)
        {            
            distanceTravelled += speed * Time.deltaTime;
            VertexPath path = pathCreator.path;
            transform.position = path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            
            if (distanceTravelled > path.length && endOfPathInstruction == EndOfPathInstruction.Stop) 
                Destroy(gameObject);
            
            //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);            
            
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}