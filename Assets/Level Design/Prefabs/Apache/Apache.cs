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

    Shooter shooter;

    Transform groundLevel;
    LayerMask layerMask;

    Animator animator;

    void Start()
    {
        shooter = GetComponent<Shooter>();
        layerMask = LayerMask.GetMask("Platforms");
        rotors = transform.Find("Apache/main Rotor Housing/Main Rotors");
        tailRotors = transform.Find("Apache/tail rotor housing/tail rotor spindle");
        groundLevel = GameObject.Find("GroundLevel").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        bool fire1 = Input.GetButton("Fire1");
        bool fire2 = Input.GetButton("Fire2");
        bool dodge = Input.GetButton("Jump"); ;


        AnimateRotors();
        if (fire1) CheckShoot();
        if (fire2) CheckBombDrop();
        if (dodge) CheckDodge();

        float delta = Time.deltaTime;
        transform.Translate(moveSpeed.x * inputX * delta, moveSpeed.y * inputY * delta, 0, Space.World);
        transform.rotation = Quaternion.Euler(0, 0, inputX * -3);
        CheckBounds();

    }

    void CheckBounds()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPos.x > 1) screenPos.x = 1;
        else if (screenPos.x < 0) screenPos.x = 0;

        if (screenPos.y < 0) screenPos.y = 0;
        else if (screenPos.y > 1) screenPos.y = 1;

        transform.position = Camera.main.ViewportToWorldPoint(screenPos);

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f, layerMask))
        {
            float y = 1.4f - hit.distance;
            if (y > 0)
                transform.Translate(new Vector3(0, y, 0));
        }
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

    void CheckShoot()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > fireSpeed)
        {
            bulletTimer = 0;
            frontSpawnPoint.Spawn();
        }
    }

    void CheckDodge()
    {
        /*
        dodgeTimer += Time.deltaTime;

        if (dodgeTimer < dodgeCycleTime)
        {
            //transform.rotation =  Quaternion.Euler(360 * dodgeCycleTime * Time.deltaTime, 0, 0);
        }
        else
        {
            dodgeTimer = 0;
            //transform.rotation =  Quaternion.Euler(0, 0, 0);
            if (animator != null)// && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                //animator.SetTrigger("jump");
            }
        }
        */
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collidedWith = other.gameObject;
        Debug.Log($"Collision {collidedWith.layer}");

        switch (collidedWith.layer)
        {

            // platform
            case 7: Debug.Log("platform crash!"); break;

            // Collidable
            case 10:
                Debug.Log("POWER UP!");

                // Power Up
                PowerUp powerUp = collidedWith.GetComponent<PowerUp>();
                if (powerUp != null)
                {
                    shooter.powerUp(powerUp.powerUpType);
                    Destroy(collidedWith);
                }

                break;

        }
    }
}
