using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    */

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
}
