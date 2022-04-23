using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUtility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    static public bool isOnCamera(Transform transform)
    {
        Vector3 spawnPos = transform.position;
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(spawnPos);
        bool onScreen =
            screenPoint.z > 0 &&
            screenPoint.x > 0 && screenPoint.x < 1.1f &&
            screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }
}
