using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float velocity;
    public float spinSpeed;

    void Start() { }

    void Update()
    {
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        transform.Rotate(Vector3.forward, Time.deltaTime * spinSpeed);
    }

    /*
        void OnTriggerEnter(Collider other)
        {
            GameObject collidedWith = other.gameObject;

            if (collidedWith != null)
            {
                switch (collidedWith.tag)
                {
                    case "obstacle": Explode(); break; 
                }
            }
        } 

    void Explode()
    {
        GameObject explodeObject = Instantiate<GameObject>(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explodeObject, 2);
    }
    */

}
