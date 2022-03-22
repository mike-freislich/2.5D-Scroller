using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SheepDetection : MonoBehaviour
{



    [Range(0, 5)]
    public float runSpeed = 3f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public ParticleSystem shockedFX;
    public GameObject smokePuffFX;
    public AudioClip popClip;
    public AudioClip sheepEatenClip;
    public AudioClip sheepAlertClip;


    private bool grounded = true;
    private bool startingRightFacing = true;
    // private float jumpHeight = 2f;
    private bool alert = false;
    private Rigidbody2D rigid;
    private Animator anim;
    private bool beingEaten;
    private bool facingRight = true;
    private Vector3 sheepScale;
    private int randomDirection;
    private AudioSource aud;




    void Start()
    {

        rigid = GetComponentInChildren<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        aud = GetComponent<AudioSource>();

        startingRightFacing = (Random.value > 0.5f);

        if (!startingRightFacing)
        {
            facingRight = startingRightFacing;
            FlipSheep();
        }

    }

    void FixedUpdate()
    {
        if (beingEaten)
            return;
        SheepMovement();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !alert)
        {
            StartCoroutine(JumpDelay());
        }

        if (col.gameObject.CompareTag("DeathZone") || col.gameObject.CompareTag("Trap") )
        {
            SheepDied();
        }
    }

    IEnumerator JumpDelay()
    {
        shockedFX.Play();
        aud.PlayOneShot(sheepAlertClip);
        anim.SetTrigger("Shocked");
        yield return new WaitForSeconds(0.15f);
        alert = true;
        SheepDirectionDetect();
    }

    void SheepMovement()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, groundLayer);

        if (grounded && alert)
        {
            rigid.velocity = new Vector2(runSpeed, 0);
            anim.SetBool("Run", grounded);
        }

    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Trap"))
        {
            SheepDied();
        }
    }




    public void SheepFreeze(bool isFlipped)
    {
        if (!isFlipped)
            FlipSheep();

        beingEaten = true;
        rigid.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Eat");
        aud.PlayOneShot(sheepEatenClip);
        Destroy(gameObject, .75f);

    }

    void FlipSheep()
    {
        //        Debug.Log("Flipping sheep");
        sheepScale = transform.localScale;
        sheepScale.x *= -1;
        transform.localScale = sheepScale;
    }

    void SheepDirectionDetect()
    {
        if (!facingRight)
        {
            FlipSheep();
        }
    }

    public void SheepDied()
    {
        if (!beingEaten)
        {
            
            rigid.bodyType = RigidbodyType2D.Static;
            Debug.Log("Sheep Died");
            Instantiate(smokePuffFX, transform.position, Quaternion.identity);
            if(popClip !=null)
             AudioSource.PlayClipAtPoint(popClip, transform.position);
            Destroy(gameObject);
        }
    }




}
