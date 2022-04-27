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

    TheGame theGame;

    void Start()
    {
        theGame = TheGame.Instance;
        StartCoroutine(MyTimer.Start(0.25f, true, () => { if (!isOnCamera) RemoveObject(); }));
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;
        if (otherObject != null)
        {
            switch (otherObject.tag)
            {
                case "obstacle": Explode(); break;
                case "Player":
                    if (this.tag == "enemy bullet") Explode();
                    break;
                case "player bullet":
                    if (this.tag == "enemy bullet")
                    {
                        theGame.AddScore(50);
                    }
                    break;
            }
        }
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

    bool isOnCamera { get { return ScreenUtility.isOnCamera(transform); }}

    void RemoveObject()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
