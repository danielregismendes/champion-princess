using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float maxSpeed = 4; //public sem uso externo. setar private, utilizar SerializeField ou melhor ainda, expor em ScriptableObject
    public float jumpForce = 400;//public sem uso externo. setar private, utilizar SerializeField ou melhor ainda, expor em ScriptableObject
    public float minHeight, maxHeight;//public sem uso externo. setar private, utilizar SerializeField ou melhor ainda, expor em ScriptableObject
    public int maxHealth = 10;//public sem uso externo. setar private, utilizar SerializeField ou melhor ainda, expor em ScriptableObject
    public string playerName;//atributo privato, get público
    public Sprite playerImage;//Idem
    public AudioClip collisionSound, jumpSound, healthItem; //public sem uso externo.
    public Weapon weapon; //public sem uso externo.

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
    private GameManager gameManager;

    private Boss bossFigth;

    [System.Obsolete]
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //ver comentário sobre singleton
        maxHealth = gameManager.GetMaxHP(); //configuração estranha, difícil de explicar em comentário, me chama pra eu dar mais detalhes
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        currentSpeed = maxSpeed;
        audioS = GetComponent<AudioSource>();
        bossFigth = FindObjectOfType<Boss>();

        
    }

    [System.Obsolete]
    void Update()
    {

        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        anim.SetBool("OnGround", onGround); //setar apenas se valor mudou
        anim.SetBool("Dead", isDead);//setar apenas se valor mudou
        anim.SetBool("Weapon", holdingWeapon);//setar apenas se valor mudou

        if (!isDead && !stop)
        {

            if (Input.GetButtonDown("Jump") && onGround && canJump) //ver InputSystem package
            {

                Jump = true;
               
            }

        }

    }

    private void FixedUpdate()
    {

        if(!isDead && !stop && !direcaoFixa)
        {
            float h = Input.GetAxis("Horizontal"); //InputSystem
            float z = Input.GetAxis("Vertical");//InputSystem

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

        float minWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 15)).x; //guardar ref da câmera, essa chamada é pesada.
        float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 15)).x;//guardar ref da câmera, essa chamada é pesada.

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
            FindObjectOfType<UIManager>().UpdateHealt(currentHealth); //evitar FindObject, ver padrão singleton
            if(currentHealth <= 0)
            {
                isDead = true;
                FindObjectOfType<GameManager>().SetLives();//evitar FindObject, ver padrão singleton
                FindObjectOfType<UIManager>().UpdateLives();//evitar FindObject, ver padrão singleton
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
            FindAnyObjectByType<UIManager>().UpdateHealt(currentHealth);    //evitar FindObject, ver padrão singleton
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
            if(!bossFigth) FindObjectOfType<UIManager>().GameOver();//evitar FindObject, ver padrão singleton

        }
    }

    public void PlaySong(AudioClip clip)
    {

        audioS.clip = clip;
        if (gameManager.GetSoundFX() && audioS) audioS.Play();

    }

    public void SetHoldingWeaponToFalse() //desnecessário. Usar atributo privado com setter público.
    {
        holdingWeapon = false; 
    }

    public bool GetHoldingWeapon() 
    {
    
        return holdingWeapon;

    }

    public void SetStop() //renomear. O nome sugere que isso é um setter, mas tu tá na verdade invertendo o valor que já existe
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

    public void SetCanFlip() // renomear ou transformar em setter.
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

    public void SetDireçãoFixa() //Idem
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

    public int GetHealth()
    {
        return currentHealth;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

}


