using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class CharacterMovement : MonoBehaviour
{
    [Range(0, 20)]
    public float maxSpeed = 6;
    [Range(0, 100)]
    public float jumpForce = 20f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public ParticleSystem respawnFX;
    public AudioClip eatingClip;
    public AudioClip jumpingClip;

    private AudioSource aud;
    private bool facingRight = true;
    private Animator anim;
    private Rigidbody2D rigid;
    private Vector3 playerScale;
    [SerializeField]
    private bool grounded = false;
    [SerializeField]
    private bool eating = false;
    private float groundRadius = 0.2f;
    private float move;
    private float originalSpeed;

    public UnityEvent OnEatingSheep;

    public delegate void EatSheep();
    public static event EatSheep OnEatSheep;

    void OnEnable()
    {

        eating = false;
        if (rigid != null)
            rigid.bodyType = RigidbodyType2D.Dynamic;

    }

    void Start()
    {

        aud = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        originalSpeed = maxSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        move = Input.GetAxis("Horizontal");
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        MelvinJump();


    }

    void FixedUpdate()
    {
        MelvinRun();

    }

    void MelvinRun()
    {

        anim.SetBool("Ground", grounded);


        if (grounded && !eating)
        {
            rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

            AnimationMovements();
        }


        if (move > 0 && !facingRight)
        {
            Flip();
        }

        else if (move < 0 && facingRight)
        {
            Flip();
        }


    }

    void MelvinJump()
    {
        if (Input.GetButtonDown("Jump") && grounded && !eating)
        {
            anim.SetBool("Jump", true);
            rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Invoke("JumpReset", 0.15f);

            if (jumpingClip != null)
                aud.PlayOneShot(jumpingClip);

        }
    }

    void JumpReset()
    {
        anim.SetBool("Jump", false);
    }

    void AnimationMovements()
    {
        anim.SetFloat("Movement", Mathf.Abs(move));

    }

    void Flip()
    {
        if (eating)
            return;

        facingRight = !facingRight;
        playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;

    }

    void Eating()
    {
        StartCoroutine(EatDelay());
    }

    IEnumerator EatDelay()
    {

        eating = true;
        anim.SetFloat("Movement", 0);
        anim.SetTrigger("Eat");
        yield return new WaitForSeconds(.5f);

        if (eatingClip != null)
            aud.PlayOneShot(eatingClip);

        yield return new WaitForSeconds(1.15f);
        rigid.bodyType = RigidbodyType2D.Dynamic;
        eating = false;

        OnEatingSheep.Invoke();

        if (OnEatSheep != null)
            OnEatSheep();

    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Sheep"))
        {
            if (!grounded)
                col.gameObject.GetComponent<SheepDetection>().SheepDied();
            else 
            {
                CheckPlayerDirection(col.transform);
     
            }

        }

    }

    void CheckPlayerDirection(Transform sheep)
    {
        if((sheep.localScale.x == transform.localScale.x) && (transform.position.x > sheep.position.x))
        {
            sheep.GetComponent<SheepDetection>().SheepDied();
            
        }
        else
        {
            sheep.GetComponent<SheepDetection>().SheepFreeze(facingRight);
            Eating();
        }
    }



    public void PlayerFreeze()
    {
        eating = true;
        anim.SetFloat("Movement", 0);
        rigid.velocity = new Vector2(0, 0);

    }

    public void PlayRespawnFX()
    {
        if (respawnFX != null)
            respawnFX.Play();
    }



}
