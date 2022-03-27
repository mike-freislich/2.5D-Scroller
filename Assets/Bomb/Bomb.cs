using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;
    public float accelerationDelay = 0.3f;
    public float acceleration = 0.9f;
    public float maxVelocity = 1.0f;
    float speed = 0;
    float delayTimer = 0;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(2, -2, 0);
    }

    void Update()
    {
        delayTimer += Time.deltaTime;
        if (delayTimer > accelerationDelay)
        {
            speed = Mathf.Min(speed + (acceleration * Time.deltaTime), maxVelocity);
            transform.Translate(Vector3.right * speed, Space.World);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case 7: Explode(); break;
            case 9: 
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.TakeDamage(100);
                Explode();
                break;
        }
    }

    void Explode()
    {
        GameObject explodeObject = Instantiate<GameObject>(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explodeObject, 2);
    }
}
