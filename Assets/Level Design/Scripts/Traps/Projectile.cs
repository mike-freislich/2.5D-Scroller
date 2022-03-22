using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Range(0, 0.1f)]
    public float movementSpeed = 0.075f;
    [Range(0, 3)]
    public float rotateSpeed = 1;
    [Range(0,5)]
    public float lifetime = 3f;
    public GameObject smokePuffFX;

    void Start()
    {
        smokePuffFX.SetActive(false);
        Invoke("DisplayFX", lifetime);

    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * movementSpeed);
        transform.Rotate(Vector3.left * rotateSpeed);
    }

    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }

    void DisplayFX()
    {
        smokePuffFX.SetActive(true);
        Destroy(gameObject, 0.33f);
    }
}
