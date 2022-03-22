using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    private Vector3 offset;
    private Vector3 fixedCam;


    void Start()
    {
        fixedCam = new Vector3(player.position.x, transform.position.y, transform.position.z);
         offset = transform.position - fixedCam;
    }

    void LateUpdate()
    {
        fixedCam = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = fixedCam + offset;
    }
}