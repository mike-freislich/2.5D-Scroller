using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apache : MonoBehaviour
{
    public GameObject bomb;
    public GameObject bullet;
    

    public float rotorSpeed;
    public Vector2 moveSpeed = new Vector2(5, 4);

    Transform rotors;
    Transform tailRotors;

    public float fireSpeed = 0.5f;
    float bombTimer;
    float bulletTimer;

    void Start()
    {
        rotors = transform.Find("main Rotor Housing/Main Rotors");
        tailRotors = transform.Find("tail rotor housing/tail rotor spindle");
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        bool fire1 = Input.GetButton("Fire1");
        bool fire2 = Input.GetButton("Fire2");
        
        AnimateRotors();
        if (fire1) CheckShoot();
        if (fire2) CheckBombDrop();

        float delta = Time.deltaTime;                
        transform.Translate(moveSpeed.x * inputX * delta, moveSpeed.y * inputY * delta, 0, Space.World);
        transform.Translate(2 * delta, 0, 0, Space.World);
        //transform.rotation =  Quaternion.Euler(-90 , 180 + inputX * -20, -90);
    }

    void AnimateRotors()
    {
        rotors.Rotate(Vector3.forward * Time.deltaTime * rotorSpeed);
        tailRotors.Rotate(Vector3.forward * Time.deltaTime * rotorSpeed);
    }

    void CheckBombDrop()
    {
        bombTimer += Time.deltaTime;
        if (bombTimer > fireSpeed)
        {
            bombTimer = 0;
            Instantiate<GameObject>(bomb, transform.position, transform.rotation);
        }
    }

    void CheckShoot() {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > fireSpeed) {
            bulletTimer = 0;
            Instantiate<GameObject>(bullet, transform.position, transform.rotation);
        }
    }
}
