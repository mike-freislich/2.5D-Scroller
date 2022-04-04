using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float velocity;

    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);        
    }
    void OnBecameInvisible()
    {        
        Destroy(gameObject);
    }
}
