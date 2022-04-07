using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Color color;
    public Vector3 spin;

    void Start()
    {
        
    }
        
    void Update()
    {
        transform.Rotate(spin * Time.deltaTime, Space.Self);
    }
}
