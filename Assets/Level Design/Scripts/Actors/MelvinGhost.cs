using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class MelvinGhost : MonoBehaviour
{
   
    [Range(0, 0.05f)]
    public float riseSpeed = .025f;
    private bool canFloat = false;
    private AudioSource aud;
    private float delay = 2f;

    public UnityEvent OnPlayerDeath;
    public delegate void PlayerGhost();
    public static event PlayerGhost OnPlayerGhost;


    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        StartCoroutine(MelvinFloat());
    }

    void FixedUpdate()
    {
        if (canFloat)
            transform.Translate(Vector2.up * riseSpeed);

    }


    IEnumerator MelvinFloat()
    {

        yield return new WaitForSeconds(delay);
        aud.Play();
        canFloat = true;
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
        if(OnPlayerGhost != null)
        OnPlayerGhost();

        OnPlayerDeath.Invoke();


    }


}
