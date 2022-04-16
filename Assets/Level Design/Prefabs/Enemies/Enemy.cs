using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject explosion;    
    

    void Start()
    {              
    }

    void Update()
    {        
    }    
    
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) Explode();
    }

    void Explode()
    {
        GameObject explodeObject = Instantiate<GameObject>(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explodeObject, 2);        
    }

    public bool isOnCamera
    {
        get
        {
            Vector3 spawnPos = transform.position;
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(spawnPos);
            bool onScreen =
                screenPoint.z > 0 &&
                screenPoint.x > 0 && screenPoint.x < 1.1f &&
                screenPoint.y > 0 && screenPoint.y < 1;            
            return onScreen;
        }
    }
}