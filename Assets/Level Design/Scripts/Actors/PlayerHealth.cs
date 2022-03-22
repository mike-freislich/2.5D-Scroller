using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class PlayerHealth : MonoBehaviour
{

    public GameObject deathPrefab;

    private Animator anim;
    public AudioClip smackClip;
    public AudioClip fallingDeathClip;
    private GameObject player;
    private bool isDead = false;
    private AudioSource aud;

    public UnityEvent OnPlayerHit;
    public UnityEvent OnPlayerFell;

    void OnEnable()
    {

        HungerMeter.OnHungerDeath += HungerDeathSequence;
        player = this.gameObject;
        player.SetActive(true);
        isDead = false;

        anim = GetComponentInChildren<Animator>();
        anim.SetBool("HungerDeath", false);

        aud = GetComponent<AudioSource>();

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Trap"))
        {
            if (smackClip != null)
                aud.PlayOneShot(smackClip);
            if (smackClip != null)
                AudioSource.PlayClipAtPoint(smackClip, transform.position);
            PlayerDeath();
        }

        if (col.gameObject.CompareTag("DeathZone"))
        {
            FallingDeathSequence();
        }


    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DeathZone"))
        {

            FallingDeathSequence();
        }
    }

    void PlayerDeath()
    {
        if (!isDead)
        {

            Instantiate(deathPrefab, player.transform.position, player.transform.rotation);
            player.SetActive(false);
            isDead = true;
            OnPlayerHit.Invoke();

        }
    }

    void HungerDeathSequence()
    {
        if (!isDead)
            StartCoroutine(HungerDeathAnimations());
    }

    IEnumerator HungerDeathAnimations()
    {

        player.GetComponent<CharacterMovement>().PlayerFreeze();
        anim.SetBool("HungerDeath", true);
        yield return new WaitForSeconds(2.56f);
        PlayerDeath();

    }

    void FallingDeathSequence()
    {
        if (!isDead)
            StartCoroutine(FallingDeath());
    }

    IEnumerator FallingDeath()
    {

        if (fallingDeathClip != null)      
            aud.PlayOneShot(fallingDeathClip);
   
        yield return new WaitForSeconds(3f);
        aud.Stop();

        OnPlayerFell.Invoke();
        OnPlayerHit.Invoke();

    }

}
