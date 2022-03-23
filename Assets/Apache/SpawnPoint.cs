using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject spawnObject;    
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void Spawn() {
        if (spawnObject) {
            spawnObject.transform.position = transform.position;
            Instantiate<GameObject>(spawnObject);
        }
    }
}
