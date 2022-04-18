using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject explosion;
    public int score = 50;

    TheGame theGame;

    void Start()
    {
        theGame = TheGame.Instance;
    }

    void Update()
    {
        if (didScrollOffScreen())        
            RemoveObject();        
    }

    void RemoveObject()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        RemoveObject();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) Explode();
    }

    void Explode()
    {
        if (theGame != null)
            theGame.AddScore(score);
        GameObject explodeObject = Instantiate<GameObject>(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explodeObject, 2);        
    }

    bool didScrollOffScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        return (screenPoint.x < -0.5f);
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
