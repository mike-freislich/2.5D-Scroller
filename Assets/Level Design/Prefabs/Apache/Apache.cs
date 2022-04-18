using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerManager
{
    //float[]gunRate = {2.0f,3.5f,4.0f,4.5f,5.0f};
    //float[]bombRate = {};

    int gun, bomb, speed, laser;

    PowerManager()
    {
    }

    public void AddPowerUp(PowerUpType powerUp)
    {
        switch (powerUp)
        {
            case PowerUpType.Speed: speed = Mathf.Min(speed + 1, 4); break;
            case PowerUpType.Bomb: bomb = Mathf.Min(bomb + 1, 4); break;
            case PowerUpType.Fire: gun = Mathf.Min(gun + 1, 4); break;
            case PowerUpType.Laser: laser = Mathf.Min(laser + 1, 1); break;
        }
    }

    public float gunSpeed
    {
        get
        {
            return 0;
        }
    }

}

public class Apache : MonoBehaviour
{
    public GameObject bomb;
    public GameObject bullet;
    public TheGame theGame;
    public SpawnPoint frontSpawnPoint;
    public SpawnPoint bombSpawnPoint;
    public Vector2 moveSpeed = new Vector2(5, 4);

    public float fireSpeed = 0.5f;
    float bombTimer;
    float bulletTimer;
    public float dodgeCycleTime;
    float dodgeTimer;

    Shooter shooter;
    LayerMask layerMask;
    Animator animator;

    void Start()
    {
        shooter = GetComponent<Shooter>();
        layerMask = LayerMask.GetMask("Platforms");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        bool fire1 = Input.GetButton("Fire1");
        bool fire2 = Input.GetButton("Fire2");
        bool dodge = Input.GetButton("Jump"); ;

        if (fire1) CheckShoot();
        if (fire2) CheckBombDrop();
        if (dodge) CheckDodge();

        TranslateBounded(
            new Vector3(moveSpeed.x * inputX * Time.deltaTime, moveSpeed.y * inputY * Time.deltaTime, 0));
        TiltOnMove(inputX);
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

        switch (collidedWith.tag)
        {
            case "enemy":
            case "enemy bullet":
            case "obstacle":
                Death(collidedWith.tag);
                break;

            case "pickup":
                PowerUp powerUp = collidedWith.GetComponent<PowerUp>();
                if (powerUp != null)
                {
                    shooter.powerUp(powerUp.powerUpType);
                    Destroy(collidedWith);
                }
                break;
        }
    }

    void Death(string reason)
    {
        GetComponent<TheGame>().PlayerDied(reason);        
        //Debug.Log($"DEATH :::: You were killed by {reason} ");
    }
}
