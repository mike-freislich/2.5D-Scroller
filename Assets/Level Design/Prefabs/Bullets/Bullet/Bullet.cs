using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosion;

    public float speed = 300f;

    public int damageAmount = 50;

    void Start()
    {
        StartCoroutine(MyTimer.Start(0.25f, true, () => { if (!isOnCamera) RemoveObject(); }));
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "obstacle": Explode(); break;

            case "enemy":
            case "enemy bullet":
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
        Destroy(explodeObject, 1);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    bool isOnCamera { get { return ScreenUtility.isOnCamera(transform); } }

    void RemoveObject()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }



}
