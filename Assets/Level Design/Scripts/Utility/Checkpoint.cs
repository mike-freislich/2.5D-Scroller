using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{


    public UnityEvent OnCheckpointCrossed;
    private bool crossedCheckpoint = false;
    private RespawnManager respawnScript;


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !crossedCheckpoint)
        {
    
            crossedCheckpoint = true;
            respawnScript = GetComponentInParent<RespawnManager>();
            if (respawnScript != null)
                respawnScript.respawnPoint = this.transform;

            OnCheckpointCrossed.Invoke();

        }
    }
}
