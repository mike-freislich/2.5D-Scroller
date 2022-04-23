using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (ScreenUtility.isOnCamera(transform))
        {
            dollycam dcam = Camera.main.GetComponent<dollycam>();
            dcam.speed = 0;
        }
    }
}
