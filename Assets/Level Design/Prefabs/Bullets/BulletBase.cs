using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public GameObject explosion;
    public Vector3 positionOffset;
    public float explosionTimeout = 2.0f;
    public int health = 50; 
    
    void Start()
    {
    }

    void Update()
    {        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if (otherObject != null) {
            switch (otherObject.tag)
            {
                case "obstacle": Explode(); break;
            }
        }
    }

    void Explode()
    {
        if (explosion != null) {
            GameObject newExplosion = Instantiate<GameObject>(explosion, transform.position + positionOffset, transform.rotation);            
            Destroy(gameObject);
            Destroy(newExplosion, explosionTimeout);            
        }
    } 

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
