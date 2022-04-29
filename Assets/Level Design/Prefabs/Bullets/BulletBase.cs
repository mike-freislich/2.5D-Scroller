using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public GameObject explosion;
    public Vector3 positionOffset;
    public float explosionTimeout = 2.0f;
    public int health = 50;
    public int score = 50;
    public Vector3 speed;    
    public float accelerationDelay;
    public float acceleration;
    public Vector3 rigidBodyInertia;

    TheGame theGame;
    private bool accelerating;
    private float currentSpeed;

    void Start()
    {
        theGame = TheGame.Instance;        

        if (accelerationDelay > 0)
            StartCoroutine(MyTimer.Start(accelerationDelay, () => { accelerating = true; }));

        StartCoroutine(MyTimer.Start(0.25f, true, () => { if (!isOnCamera) RemoveObject(); }));
        SetInertia();
    }

    void SetInertia()
    {
        if (rigidBodyInertia != null)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null) rb.velocity = rigidBodyInertia;
        }
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (accelerationDelay > 0)
        {
            if (accelerating)
            {
                currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.fixedDeltaTime, speed.x);                
                transform.Translate(Vector3.right * currentSpeed, Space.World);
            }
        }
        else
        {
            transform.Translate(speed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if (otherObject != null)
            HitObject(otherObject);
    }


    private void HitObject(GameObject hitObject)
    {
        switch (hitObject.tag)
        {
            case "obstacle":
                Explode();
                break;

            case "Player":
                if (this.tag == "enemy bullet")
                    Explode();
                break;


            case "enemy":
                if (this.tag == "player bullet")
                {
                    getTopLevelEnemy(hitObject)?.TakeDamage(health);
                    Explode();
                }
                break;


            case "enemy bullet":
                if (this.tag == "player bullet")
                {
                    BulletBase enemyBullet = hitObject.GetComponent<BulletBase>();
                    if (enemyBullet != null)
                    {
                        int enemyBulletHealthBeforeHit = enemyBullet.health;

                        // take health from enemy bullet                        
                        enemyBullet.health -= this.health;
                        if (enemyBullet.health <= 0)
                        {
                            theGame.AddScore(enemyBullet.score);
                            enemyBullet.Explode();
                        }

                        // take health from player bullet
                        this.health -= enemyBulletHealthBeforeHit;
                        if (this.health <= 0)
                            Explode();
                    }
                }
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
        if (explosion != null)
        {
            GameObject newExplosion = Instantiate<GameObject>(explosion, transform.position + positionOffset, transform.rotation);
            Destroy(gameObject);
            Destroy(newExplosion, explosionTimeout);
        }
    }

    void OnBecameInvisible()
    {
        RemoveObject();
    }

    bool isOnCamera { get { return ScreenUtility.isOnCamera(transform); } }

    void RemoveObject()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
