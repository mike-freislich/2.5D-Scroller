using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    public GameObject missilePrefab;
    public float shootDelay = 2.0f;
    Transform leftGun;
    Transform rightGun;
    Transform currentGun;
    Animator animator;

    float shootTimer;

    void Start()
    {
        leftGun = transform.Find("Pivot/Body/Barrel_Left/LeftGun");
        rightGun = transform.Find("Pivot/Body/Barrel_Right/RightGun");
        currentGun = leftGun;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isOnCamera)
        {
            if (!isMoving)
            {
                shootTimer += Time.deltaTime;
                if (shootTimer > shootDelay)
                    LaunchMissile();
            }
        }
    }

    bool isMoving { get { return (animator.IsInTransition(0) || animator.IsInTransition(1)); } }

    void LaunchMissile()
    {
        shootTimer = 0;
        currentGun = (currentGun == leftGun) ? rightGun : leftGun;
        GameObject missile = Instantiate(missilePrefab, currentGun.position, currentGun.rotation);
    }

    bool isOnCamera
    {
        get
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }
    }
}