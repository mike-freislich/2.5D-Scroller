using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SmashingTrap : MonoBehaviour
{
    [Range(0, 10)]
    public float smashDistance;
    [Range(0, 10)]
    public float smashDownSpeed = .15f;
    [Range(0, 10)]
    public float retractSpeed = 3f;
    [Range(0, 10)]
    public float smashDownDelay = 1f;
    private bool towerSmashing;
    private bool currentlySmashing;
    private float currentSpeed;
    private Vector3 smashDirection;
    private Vector3 originalPosition;
    private AudioSource aud;

    public UnityEvent OnTowerDown;
    public UnityEvent OnTowerUp;


    void OnEnable()
    {
        aud = GetComponent<AudioSource>();
        originalPosition = transform.localPosition;
        smashDirection = new Vector3(originalPosition.x, originalPosition.y - smashDistance, originalPosition.z);
        RespawnManager.OnRespawningPlayer += StopSmashing;
    }

    void OnDisable()
    {
        RespawnManager.OnRespawningPlayer -= StopSmashing;
    }


    public void StartSmashing()
    {
        towerSmashing = !towerSmashing;

        if (towerSmashing && !currentlySmashing)
            StartCoroutine(SmashDown());
    }

    public void StopSmashing()
    {
        towerSmashing = false;
    }


    IEnumerator SmashDown()
    {
        currentlySmashing = true;

        yield return new WaitForSeconds(smashDownDelay);

        while (currentSpeed < smashDownSpeed)
        {
            currentSpeed += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(originalPosition, smashDirection, currentSpeed / smashDownSpeed);
            yield return new WaitForEndOfFrame();
        }
        aud.Play();
        OnTowerDown.Invoke();
   
        currentSpeed = 0;
        StartCoroutine(Retract());

    }

    IEnumerator Retract()
    {

        while (currentSpeed < retractSpeed)
        {
            currentSpeed += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(smashDirection, originalPosition, currentSpeed / retractSpeed);
            yield return new WaitForEndOfFrame();
        }

        currentSpeed = 0;
        currentlySmashing = false;
        OnTowerUp.Invoke();
        if (towerSmashing)
            StartCoroutine(SmashDown());


    }
}
