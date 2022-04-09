using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum PowerUpType { Speed, Bomb, Fire, Laser };
public enum Power { Low, Medium, High, VeryHigh, Super };

public class PowerUp : MonoBehaviour
{
    public Vector3 spin;
    public PowerUpType powerUpType;

    void Start()
    {
        switch (powerUpType)
        {
            case PowerUpType.Speed: ChangeColor(new Color(0.2f, 0.2f, 1, 0.4f)); break;
            case PowerUpType.Fire: ChangeColor(new Color(1, 0, 0, 0.4f)); break;
            case PowerUpType.Bomb: ChangeColor(new Color(0, 1, 0, 0.4f)); break;
            case PowerUpType.Laser: ChangeColor(new Color(1, 1, 1, 0.4f)); break;
        }
    }

    void ChangeColor(Color c)
    {
        GameObject light = transform.Find("Light").gameObject;
        GameObject halo = transform.Find("Halo").gameObject;
        light.GetComponent<Light>().color = c;
        halo.GetComponent<Light>().color = c;

        Renderer sphereRenderer = transform.Find("Sphere").gameObject.GetComponent<Renderer>();
        sphereRenderer.material.color = c;
    }

    void Update()
    {
        transform.Rotate(spin * Time.deltaTime, Space.Self);

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("PowerUp collided");
    }
}
