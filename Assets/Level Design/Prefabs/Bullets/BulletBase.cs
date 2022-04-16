using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public GameObject explosion;
    public Vector3 positionOffset;
    public float explosionTimeout = 2.0f;
    public int health = 50;
    public Vector3 speed;

    void Start()
    {
    }

    void Update()
    {
        if (speed != null)
            transform.Translate(speed * Time.deltaTime);

        if (!isOnCamera) RemoveObject();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if (otherObject != null)
        {
            switch (otherObject.tag)
            {
                case "obstacle": Explode(); break;
            }
        }
    }

    void RemoveObject()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void Explode()
    {
        if (explosion != null)
        {
            GameObject newExplosion = Instantiate<GameObject>(explosion, transform.position + positionOffset, transform.rotation);
            Destroy(gameObject);
            Destroy(newExplosion, explosionTimeout);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    bool isOnCamera
    {
        get
        {
            Vector3 spawnPos = transform.position;
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(spawnPos);
            bool onScreen =
                screenPoint.z > 0 &&
                screenPoint.x > -0.1f && screenPoint.x < 1.1f &&
                screenPoint.y > -0.1f && screenPoint.y < 1.1f;
            return onScreen;
        }
    }
}
