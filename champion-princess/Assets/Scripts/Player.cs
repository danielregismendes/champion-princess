using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool stop = false;
    private bool canFlip = true;
    private bool canJump = true;
    private bool direcaoFixa = false;

    void Start()
    {

        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        currentSpeed = maxSpeed;
        audioS = GetComponent<AudioSource>();
        
    }

    [System.Obsolete]
    void Update()
    {

        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        anim.SetBool("OnGround", onGround);
        anim.SetBool("Dead", isDead);
        anim.SetBool("Weapon", holdingWeapon);

        if (!isDead && !stop)
        {

            if (Input.GetButtonDown("Jump") && onGround && canJump)
            {

                Jump = true;
               
            }

            if (Input.GetButtonDown("Cancel"))
            {
                FindObjectOfType<GameManager>().GameOver();
                SceneManager.LoadScene(0);
            }

        }

    }

    private void FixedUpdate()
    {

        if(!isDead && !stop && !direcaoFixa)
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
                SetDireçãoFixa();
                SetCanFlip();
                rb.AddForce(Vector3.up * jumpForce);
                PlaySong(jumpSound);
            }
        }

        float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 15)).x;
        float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 15)).x;

        rb.position = new Vector3(Mathf.Clamp(rb.position.x, minWidth+1, maxWidth-1), rb.position.y,
        Mathf.Clamp(rb.position.z, minHeight, maxHeight));
        
    }

    void Flip()
    {
        if(canFlip)
        {

            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

        }
    }

    void ZeroSpeed()
    {

        currentSpeed = 0;
        canFlip = false;
        canJump = false;
    }

    void resetSpeed()
    {

        currentSpeed = maxSpeed;
        canFlip = true;
        canJump = true;
        if (direcaoFixa) SetDireçãoFixa();
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
                FindObjectOfType<GameManager>().SetLives();
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

    [Obsolete]
    void PlayerRespawn()
    {
        if (FindObjectOfType<GameManager>().GetLives() >= 0)
            {
                isDead = false;
                currentHealth = maxHealth;
                FindAnyObjectByType<UIManager>().UpdateHealt(currentHealth);
                anim.Rebind();
                float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
                transform.position = new Vector3(minWidth, 10, -4);
            }
        else
        {
            FindObjectOfType<UIManager>().GameOver();

        }
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

    public void SetStop()
    {
        if (stop)
        {
            stop = false;
            resetSpeed();
        }
        else
        {
            stop = true;
            ZeroSpeed();
            anim.SetFloat("Speed", 0);
        }

    }

    public bool GetStop()
    {
        return stop;

    }

    public void SetCanFlip()
    {
        if (canFlip)
        {
            canFlip = false;
        }
        else
        {
            canFlip = true;
        }
    }

    public void SetDireçãoFixa()
    {
        if (direcaoFixa)
        {
            direcaoFixa = false;
        }
        else
        {
            direcaoFixa = true;
        }
    }

}


