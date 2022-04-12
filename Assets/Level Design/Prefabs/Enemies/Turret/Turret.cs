using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    Animator animator;
    Transform playerTransform;
    Vector3 playerSize;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.Find("Player").transform;
        playerSize = GameObject.Find("Player/Apache").GetComponent<MeshRenderer>().bounds.size;
    }

    void Update()
    {
        if (isOnCamera)
        {
            if (animator != null && !(animator.IsInTransition(0) || animator.IsInTransition(1)))
            {
                if ((playerTransform.position.x < transform.position.x) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Aim Left"))
                    animator.SetTrigger("aimLeft");

                if ((playerTransform.position.x > transform.position.x) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Aim Right"))
                    animator.SetTrigger("aimRight");

                if ((playerTransform.position.y > transform.position.y + playerSize.y * 2) && !animator.GetCurrentAnimatorStateInfo(1).IsName("Aim Up"))
                    animator.SetTrigger("aimUp");

                if ((playerTransform.position.y < transform.position.y + playerSize.y * 2) && !animator.GetCurrentAnimatorStateInfo(1).IsName("Aim Down"))
                    animator.SetTrigger("aimDown");
            }
        }
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
