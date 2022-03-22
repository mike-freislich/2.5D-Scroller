using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    [Range(0, 1f)]
    public float shakeDuration = 0.15f;
    [Range(0, 1f)]
    public float shakeAmount = 0.035f;
    [Range(0, 3f)]
    public float decreaseFactor = 1.0f;
    public bool soloStart;
    private float resetDuration;
    private Transform camTransform;
    private Vector3 originalPos;


    void Start()
    {
        camTransform = GetComponent<Transform>();
        originalPos = camTransform.localPosition;
        resetDuration = shakeDuration;

        if (soloStart)
        {
            StartCoroutine("ImpactShake");
        }
    }

    public void StartImpactShake()
    {
        shakeDuration = 0.15f;
        shakeAmount = 0.035f;
        StartCoroutine("ImpactShake");
    }

    public void StartFallingShake()
    {
        shakeDuration = 0.15f;
        shakeAmount = 0.15f;
        StartCoroutine("ImpactShake");
    }

    IEnumerator ImpactShake()
    {
        while (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
            yield return new WaitForEndOfFrame();
        }
        shakeDuration = resetDuration;
        camTransform.localPosition = originalPos;
    }


}