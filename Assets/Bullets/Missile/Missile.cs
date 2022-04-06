using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float velocity;
    public float spinSpeed;

    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        transform.Rotate(Vector3.forward, Time.deltaTime * spinSpeed);       
    }
    void OnBecameInvisible()
    {        
        Destroy(gameObject);
    }
}
