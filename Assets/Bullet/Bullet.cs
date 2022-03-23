using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosion;

    public float speed = 3.0f;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.right * speed, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer) {
            case 7: Explode(); break; 
            case 9:
                Explode();
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.TakeDamage(50);
                break; 
        }
    }

    void Explode()
    {
        GameObject explodeObject = Instantiate<GameObject>(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explodeObject, 2);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
