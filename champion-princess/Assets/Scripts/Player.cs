using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float maxSpeed = 4;
    public float jumpForce = 400;
    public float minHeight, maxHeight;
    public int maxHealth = 10;
    public string playerName;
    public Sprite playerImage;
    public AudioClip collisionSound, jumpSound, healthItem;
    public Weapon weapon;

    private int currentHealth;
    private float currentSpeed;
    private Rigidbody rb;
    private Animator anim;
    private Transform groundCheck;
    private bool onGround;
    private bool isDead = false;
    private bool facingRight = true;
    private bool Jump = false;
    private AudioSource audioS;
    private bool holdingWeapon = false;


    void Start()
    {

        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        currentSpeed = maxSpeed;
        audioS = GetComponent<AudioSource>();
        
    }


    void Update()
    {

        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        

        anim.SetBool("OnGround", onGround);
        anim.SetBool("Dead", isDead);
        anim.SetBool("Weapon", holdingWeapon);

        if (Input.GetButtonDown("Jump")&& onGround)
        {

            Jump = true;

        }

        if(Input.GetButtonDown("Fire1"))
        {

            anim.SetTrigger("Attack");

        }

    }

    private void FixedUpdate()
    {

        if(!isDead)
        {
            float h = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if(!onGround)
                z = 0;
                
            rb.velocity = new Vector3(h * currentSpeed, rb.velocity.y, z * currentSpeed);

            if(onGround) 
                anim.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));            

            if (h > 0 && !facingRight)
            {

                Flip();

            }
            else if(h < 0 && facingRight)
            {

                Flip();

            }


            if (Jump)
            {
                Jump = false;
                rb.AddForce(Vector3.up * jumpForce);
                PlaySong(jumpSound);
            }

            float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
            float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;

            rb.position = new Vector3(Mathf.Clamp(rb.position.x, minWidth+1, maxWidth-1), rb.position.y,
                Mathf.Clamp(rb.position.z, minHeight, maxHeight));

        }
    }

    void Flip()
    {

        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

    }

    void ZeroSpeed()
    {

        currentSpeed = 0;

    }

    void resetSpeed()
    {

        currentSpeed = maxSpeed;

    }

    [System.Obsolete]
    public void TookDamage(int damage)
    {

        if(!isDead)
        {
            currentHealth -= damage;
            anim.SetTrigger("HitDamage");
            FindObjectOfType<UIManager>().UpdateHealt(currentHealth);
            PlaySong(collisionSound);
            if(currentHealth <= 0)
            {
                isDead = true;
                FindObjectOfType<GameManager>().lives--;
                FindObjectOfType<UIManager>().UpdateLives();
                if(facingRight)
                {
                    rb.AddForce(new Vector3(-3, 5, 0), ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector3(3, 5, 0), ForceMode.Impulse);
                }
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Health Item"))
        {
            Destroy(other.gameObject);
            anim.SetTrigger("Catching");
            PlaySong(healthItem);
            currentHealth = maxHealth;
            FindAnyObjectByType<UIManager>().UpdateHealt(currentHealth);    
        }

        if (other.CompareTag("Weapon"))
        {
            anim.SetTrigger("Catching");
            holdingWeapon = true;
            WeaponItem weaponItem = other.GetComponent<PickableWeapon>().weapon;
            weapon.ActicateWeapon(weaponItem.sprite, weaponItem.color, weaponItem.durability, weaponItem.damage);
            Destroy(other.gameObject);

        }

    }

    void PlayerRespawn()
    {
        isDead = false;
        currentHealth = maxHealth;
        FindAnyObjectByType<UIManager>().UpdateHealt(currentHealth);
        anim.Rebind();
        float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0,0,10)).x;
        transform.position = new Vector3(minWidth, 10, -4);
    }

    public void PlaySong(AudioClip clip)
    {

        audioS.clip = clip;
        audioS.Play();

    }

    public void SetHoldingWeaponToFalse()
    {
        holdingWeapon = false;
    }

    public bool GetHoldingWeapon() 
    {
    
        return holdingWeapon;

    }

}


