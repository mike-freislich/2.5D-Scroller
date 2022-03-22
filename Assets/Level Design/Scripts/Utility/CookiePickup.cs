using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CookiePickup : MonoBehaviour
{
    public AudioClip popClip;

    public delegate void CookieEaten();
    public static event CookieEaten OnCookieEaten;
 
    void Update()
    {
        transform.Rotate(Vector2.up * 1f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(popClip, transform.position);
            if (OnCookieEaten != null)
                OnCookieEaten();

            Destroy(gameObject);
        }
    }
}
