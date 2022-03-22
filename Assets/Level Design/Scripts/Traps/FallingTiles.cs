using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class FallingTiles : MonoBehaviour
{

    private Rigidbody2D rigid;
    private Vector3 originalPosition;
    public bool hasFallen;
    private AudioSource aud;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.bodyType = RigidbodyType2D.Static;
        rigid.isKinematic = true;
        originalPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !hasFallen)
        {
            StartCoroutine(FallingCycle());
        }
    }


    IEnumerator FallingCycle()
    {

        aud.Play();
        yield return new WaitForSeconds(0.75f);
        hasFallen = true;
        rigid.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(3f);
        rigid.bodyType = RigidbodyType2D.Static;
        transform.position = originalPosition;
        hasFallen = false;


    }
}
