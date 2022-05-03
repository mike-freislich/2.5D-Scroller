using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apache : MonoBehaviour
{
    float[] speeds = { 0.7f, 0.85f, 1.0f, 1.1f, 1.3f };
    float[] bombSpeeds = { 5, 4, 3, 2, 1 };
    float[] gunSpeeds = { 5, 4, 3, 2, 1 };
    public TheGame theGame;
    public GameObject bomb, bullet;
    public SpawnPoint frontSpawnPoint, bombSpawnPoint;
    public Vector2 moveSpeed = new Vector2(6, 5);
    public float dodgeCycleTime, fireSpeed = 0.5f;
    public int bombPower, gunPower, speedPower;
    public bool laser = false;

    float bombTimer, bulletTimer, dodgeTimer;
    LayerMask layerMask;
    Animator animator;

    void Start()
    {
        layerMask = LayerMask.GetMask("Platforms");
        animator = GetComponent<Animator>();
        theGame = GetComponent<TheGame>();
    }

    void Update()
    {
        //MonitorButtons();
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        bool fire1 = Input.GetButton("Fire1");
        bool fire2 = Input.GetButton("Fire2");
        //bool dodge = Input.GetButton("Jump");

        if (fire1) CheckShoot();
        if (fire2) CheckBombDrop();

        Vector2 speed = moveSpeed * speeds[speedPower];
        TranslateBounded(new Vector3(speed.x * inputX * Time.deltaTime, speed.y * inputY * Time.deltaTime, 0));
        TiltOnMove(inputX);
    }

    void MonitorButtons()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                print("joystick 1 button " + i);
            }
        }
    }

    void TiltOnMove(float amount)
    {
        transform.rotation = Quaternion.Euler(Mathf.Clamp(amount * 10, -5, 10), 90, 0);
    }

    void TranslateBounded(Vector3 translateBy)
    {
        // Keep Player on Screen     
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position + translateBy);
        if ((screenPos.x < 0 && translateBy.x < 0) || (screenPos.x > 1 && translateBy.x > 0)) translateBy.x = 0;
        if ((screenPos.y < 0 && translateBy.y < 0) || (screenPos.y > 0.92f && translateBy.y > 0)) translateBy.y = 0;
        transform.Translate(translateBy, Space.World);

        // Keep Player above Platforms
        Vector3 offset = Vector3.up * 1.4f;
        Ray ray = new Ray(transform.position + offset, Vector3.down);
        Debug.DrawRay(ray.origin, ray.direction * (offset.y + 1), Color.red, 0.25f, true);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, offset.y + 1, layerMask))
        {
            float y = offset.y + 0.2f - hit.distance;
            if (y > 0) transform.Translate(new Vector3(0, y, 0), Space.World);
        }
    }

    void AddPowerUp(PowerUpType powerUp)
    {
        switch (powerUp)
        {
            case PowerUpType.Speed: speedPower++; speedPower = Mathf.Min(speedPower, 4); break;
            case PowerUpType.Bomb: bombPower++; bombPower = Mathf.Min(bombPower, 4); break;
            case PowerUpType.Gun: gunPower++; gunPower = Mathf.Min(gunPower, 4); break;
            case PowerUpType.Laser: laser = true; break;
        }
    }

    void CheckBombDrop()
    {
        bombTimer += Time.deltaTime;
        if (bombTimer > fireSpeed * bombSpeeds[bombPower])
        {
            bombTimer = 0;
            bombSpawnPoint.Spawn();
        }
    }

    void CheckShoot()
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > fireSpeed * gunSpeeds[gunPower])
        {
            bulletTimer = 0;
            frontSpawnPoint.Spawn();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collidedWith = other.gameObject;

        switch (collidedWith.tag)
        {
            case "enemy":
            case "enemy bullet":
            case "obstacle":
                Death(collidedWith.tag);
                break;

            case "pickup":
                PowerUp powerUp = collidedWith.GetComponent<PowerUp>();
                if (powerUp != null) AddPowerUp(powerUp.powerUpType);
                Destroy(collidedWith);
                break;
        }
    }

    void Death(string reason)
    {
        theGame.PlayerDied(reason);
    }
}
