using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apache : MonoBehaviour
{
    public GameObject bomb;
    public GameObject bullet;

    public SpawnPoint frontSpawnPoint;
    public SpawnPoint bombSpawnPoint;
    

    public float rotorSpeed;
    public Vector2 moveSpeed = new Vector2(5, 4);

    Transform rotors;
    Transform tailRotors;

    public float fireSpeed = 0.5f;
    float bombTimer;
    float bulletTimer;

    public float dodgeCycleTime;
    float dodgeTimer;    

    //GameObject parent;

    void Start()
    {
        //parent = GameObject.Find("Player");                
        rotors = transform.Find("Apache/main Rotor Housing/Main Rotors");
        tailRotors = transform.Find("Apache/tail rotor housing/tail rotor spindle");
    }

    void Update()
    {   
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        bool fire1 = Input.GetButton("Fire1");
        bool fire2 = Input.GetButton("Fire2");
        //bool dodge = Input.GetKeyDown(" ");


        
        AnimateRotors();
        if (fire1) CheckShoot();
        if (fire2) CheckBombDrop();
        //if (dodge) CheckDodge();

        float delta = Time.deltaTime;                
        transform.Translate(moveSpeed.x * inputX * delta, moveSpeed.y * inputY * delta, 0, Space.World);       
        transform.rotation =  Quaternion.Euler(0, 0, inputX * -3);
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
            bombSpawnPoint.Spawn();    
        }
    }

    void CheckShoot() {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > fireSpeed) {
            bulletTimer = 0;
            frontSpawnPoint.Spawn();
        }
    }

    void CheckDodge() {
        dodgeTimer += Time.deltaTime;

        if (dodgeTimer < dodgeCycleTime) {            
            transform.rotation =  Quaternion.Euler(360 * dodgeCycleTime * Time.deltaTime, 0, 0);
        } else {
            transform.rotation =  Quaternion.Euler(0, 0, 0);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 7) { // Platforms
            Debug.Log("platform crash!");
        }
    }
}
