using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BossState { idle, active, dying, dead };

public class BossTank : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject bullet;
    public float distance;
    public float moveSpeed;
    public float bulletSpeed;
    public float shootSpeedMin;
    public float shootSpeedMax;

    BossState bossState = BossState.idle;
    float shootSpeed;
    float shootTimer;
    float travelled;
    Vector3 direction;
    Scroll_Track trackLeft, trackRight;
    Vector3 startPoint, endPoint;



    void Start()
    {
        trackLeft = transform.Find("Chi_Ha_Track_L").gameObject.GetComponent<Scroll_Track>();
        trackRight = transform.Find("Chi_Ha_Track_R").gameObject.GetComponent<Scroll_Track>();
        trackLeft.scrollSpeed = 0;
        trackRight.scrollSpeed = 0;
        direction = Vector3.right;

        startPoint = transform.position;
        endPoint = startPoint + direction * distance;
        StartShootTimer();
    }

    void Update()
    {
        switch (bossState)
        {
            case BossState.idle:
                if (ScreenUtility.isOnCamera(transform)) bossState = BossState.active;
                break;

            case BossState.active:
                shootTimer += Time.deltaTime;
                if (shootTimer > shootSpeed) Shoot();
                Move();
                break;
        }
    }

    void Shoot()
    {
        foreach(GameObject spawnPoint in spawnPoints) {
            Transform t = spawnPoint.transform;
            NewBullet(t);
        }

        StartShootTimer();
    }

    void NewBullet(Transform t)
    {
        GameObject newBullet = Instantiate<GameObject>(bullet);
        BulletBase bb = newBullet.GetComponent<BulletBase>();
        bb.transform.position = t.position;
        bb.transform.rotation = t.rotation;
        bb.speed = Vector3.forward * bulletSpeed;        
    }

    void Move()
    {
        travelled += moveSpeed * direction.x * Time.deltaTime;
        if (direction.x > 0 && travelled > distance) ChangeDirection(distance);
        else if (direction.x < 0 && travelled < 0) ChangeDirection(0);

        transform.position = startPoint + Vector3.right * travelled;
    }

    void ChangeDirection(float distance)
    {
        travelled = distance;
        direction.x *= -1;
        trackLeft.scrollSpeed = moveSpeed * 0.01f * direction.x;
        trackRight.scrollSpeed = moveSpeed * 0.01f * direction.x;
    }

    void StartShootTimer()
    {
        shootTimer = 0;
        shootSpeed = Random.Range(shootSpeedMin, shootSpeedMax);
    }
}
