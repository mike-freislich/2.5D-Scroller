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
    int damageAmount = 100;

    void Start()
    {    
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(2, -2, 0);
    }

    void FixedUpdate()
    {
        delayTimer += Time.fixedDeltaTime;
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
        switch (other.gameObject.tag)
        {
            case "obstacle": Explode(); break;
            case "enemy":
            case "enemy bullet":
                Enemy enemy = getTopLevelEnemy(other.gameObject);
                if (enemy != null) enemy.TakeDamage(damageAmount);
                Explode();
                break;
        }
    }

    Enemy getTopLevelEnemy(GameObject hitObject)
    {
        Enemy enemy = hitObject.GetComponent<Enemy>();        
        if (enemy == null)
            enemy = hitObject.GetComponentInParent<Enemy>();        
        return enemy;
    }

    void Explode()
    {
        GameObject explodeObject = Instantiate<GameObject>(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explodeObject, 2);
    }

}
