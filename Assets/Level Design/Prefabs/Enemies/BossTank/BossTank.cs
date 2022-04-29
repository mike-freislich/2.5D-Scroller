using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossTank : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject bullet;
    public float distance;
    public float moveSpeed;
    public float bulletSpeed;
    public float shootSpeedMin;
    public float shootSpeedMax;

    Boss boss;
    float shootSpeed;
    float travelled;
    Vector3 moveDirection;
    Scroll_Track trackLeft, trackRight;
    Vector3 startPoint, endPoint;

    void Start()
    {
        InitialiseBoss();
        StartVectors();
        SetupTankTracks();
    }

    void InitialiseBoss()
    {
        boss = gameObject.GetComponent<Boss>();
        boss.OnEnteredCameraView = ActivateAI;
        boss.OnMove = Move;
        boss.timeRemaining = 30;
    }

    void StartVectors()
    {
        moveDirection = Vector3.right;
        startPoint = transform.position;
        endPoint = startPoint + moveDirection * distance;
    }

    void SetupTankTracks()
    {
        trackLeft = transform.Find("Chi_Ha_Track_L").gameObject.GetComponent<Scroll_Track>();
        trackRight = transform.Find("Chi_Ha_Track_R").gameObject.GetComponent<Scroll_Track>();
        trackLeft.scrollSpeed = 0;
        trackRight.scrollSpeed = 0;
    }

    void Update()
    {

    }

    void ActivateAI()
    {
        boss.bossState = BossState.active;
        Shoot();
    }

    void Shoot()
    {
        foreach (GameObject spawnPoint in spawnPoints)
        {
            Transform t = spawnPoint.transform;
            NewBullet(t);
        }

        if (boss.bossState == BossState.active)
        {
            shootSpeed = Random.Range(shootSpeedMin, shootSpeedMax);
            StartCoroutine(MyTimer.Start(shootSpeed, Shoot));
        }
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
        travelled += moveSpeed * moveDirection.x * Time.deltaTime;

        if (moveDirection.x > 0 && travelled > distance) ChangeDirection(distance);
        else if (moveDirection.x < 0 && travelled < 0) ChangeDirection(0);

        transform.position = startPoint + Vector3.right * travelled;        
    }

    void ChangeDirection(float distance)
    {
        travelled = distance;
        moveDirection.x *= -1;
        trackLeft.scrollSpeed = moveSpeed * 0.01f * moveDirection.x;
        trackRight.scrollSpeed = moveSpeed * 0.01f * moveDirection.x;
    }
}