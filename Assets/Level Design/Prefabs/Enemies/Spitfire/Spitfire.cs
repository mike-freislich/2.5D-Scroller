using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitfire : MonoBehaviour
{
    public float speed;
    public float loopDelay;
    public float rollDelay;
    public float fireRate;
    public GameObject bullet;

    bool moving;
    Enemy enemy;
    Animator animator;
    Transform propeller, gun_left, gun_right;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        propeller = transform.Find("propeller");
        gun_left = transform.Find("gun_left");
        gun_right = transform.Find("gun_right");

        if (bullet != null)
        {
            //bullet.tag = "Enemy Bullet";  
            //bullet.layer = 1;
        }

    }

    void Update()
    {
        if (!moving && enemy != null && enemy.isOnCamera) StartMove();

        if (moving)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            if (propeller != null)
                propeller.Rotate(Vector3.up * 1000 * Time.deltaTime, Space.Self);
        }
    }

    void StartMove()
    {
        moving = true;
        if (rollDelay >= 0) StartCoroutine(MyTimer.Start(rollDelay, false, Roll));
        if (loopDelay >= 0) StartCoroutine(MyTimer.Start(loopDelay, false, Loop));
        if (fireRate > 0) StartCoroutine(MyTimer.Start(fireRate, true, Shoot));
    }

    void Roll()
    {
        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            animator.SetTrigger("Roll");
    }
    void Loop()
    {
        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            animator.SetTrigger("Loop");
    }

    void Shoot()
    {
        GameObject bulletGameObject = Instantiate<GameObject>(bullet);
        BulletBase newBullet = bullet.GetComponent<BulletBase>();
        bulletGameObject.transform.position = gun_left.position;        
        if (newBullet != null) newBullet.speed = new Vector3(-20,0,0);
    }
}
