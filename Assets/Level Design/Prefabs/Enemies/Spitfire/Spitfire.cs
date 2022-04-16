using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitfire : MonoBehaviour
{
    public float speed;
    public float loopDelay;
    public float rollDelay;

    bool moving;
    Enemy enemy;
    Animator animator;

    Transform propeller;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        propeller = transform.Find("propeller");       
    }

    void Update()
    {
        if (!moving && enemy != null && enemy.isOnCamera) StartMove();

        if (moving) {
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
    }

    void Roll()
    {
        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Roll");
        }
    }
    void Loop()
    {
        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Loop");
        }
    }
}
