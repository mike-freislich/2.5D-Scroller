using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ShootingTrap : MonoBehaviour
{

    [Range(0, 5)]
    public float launchTimer = 3f;
    public Transform launchOrigin;
    public GameObject shootablePrefab;
    public bool soloStart;
    private bool playerDetected = false;
    private bool shooting = false;
    private AudioSource aud;

    void OnEnable()
    {
       
        aud = GetComponent<AudioSource>();
        RespawnManager.OnRespawningPlayer += StopLaunching;
    }

    void OnDisable ()
    {
        RespawnManager.OnRespawningPlayer -= StopLaunching;  
    }

    void Start ()
    {
        if(soloStart)
        LaunchSpikeBall();
    }

    public void LaunchSpikeBall()
    {
        playerDetected = !playerDetected;

        if (playerDetected && !shooting)
        {
            StartCoroutine(LaunchProjectile());
        }

    }

   public void StopLaunching()
    {
        playerDetected = false;
        shooting = false;
        StopAllCoroutines();
    }

    IEnumerator LaunchProjectile()
    {
        shooting = true;

        while (playerDetected)
        {
            yield return new WaitForSeconds(launchTimer);
            aud.Play();
            Instantiate(shootablePrefab, launchOrigin.position, launchOrigin.rotation);
        }
        shooting = false;
    }
}
