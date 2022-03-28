using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosion;

    public float speed = 3.0f;

    public int damageAmount = 50;

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
                Enemy enemy = getTopLevelEnemy(other.gameObject);
                if (enemy != null) enemy.TakeDamage(damageAmount);
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

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
