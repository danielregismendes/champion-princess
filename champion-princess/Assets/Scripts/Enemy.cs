using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Enemy : MonoBehaviour {
	//Muitos atributos em comum com o player. Avaliar a possibilidade de usar uma classe pai comum ou expor esses atributos em scriptable object.
	public float maxSpeed;
	public float minHeight, maxHeight;
	public float damageTime = 0.5f;
	public int maxHealth;
	public float attackRate = 1f;
	public string enemyName;
	public Sprite enemyImage;
	public AudioClip collisionSound, deathSound;
	public EnemyHit enemyHit;
    public Attack attack;
	public bool boboTreino = false;
	public bool boss = false;

    public Color damageColor;
    public float damageExibitionTime = 0.1f;
	public GameObject damageText;
    public Transform damageTextPosition;

    private int currentHealth;
	private float currentSpeed;
	private Rigidbody rb;
	protected Animator anim;
	private Transform groundCheck;
	private bool onGround;
	protected bool facingRight = false;
	private Transform target;
	protected bool isDead = false;
	private float zForce;
	private float walkTimer;
	private bool damaged = false;
	private float damageTimer;
	private float nextAttack;
	private AudioSource audioS;
	public bool stop = false;
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
	private CameraFollow cam;
	private GameManager	gameManager;

	public float distanceAttackX = 1.5f;
    public float distanceAttackZ = 1.5f;



    [System.Obsolete]
    void Start () {

		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		groundCheck = transform.Find("GroundCheck");
		target = FindObjectOfType<Player>().transform;
		currentHealth = maxHealth;
		audioS = GetComponent<AudioSource>();
		cam = FindObjectOfType<CameraFollow>();
        gameManager = FindObjectOfType<GameManager>();

    }
	

	void Update () {
		if (!boboTreino)
		{
			onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
			anim.SetBool("Grounded", onGround); //setar apenas se o valor mudou
			anim.SetBool("Dead", isDead); //setar apenas se o valor mudou

			if (!isDead && !stop)
			{
				facingRight = (target.position.x < transform.position.x) ? false : true;
				if (facingRight)
				{
					transform.eulerAngles = new Vector3(0, 180, 0);
				}
				else
				{
					transform.eulerAngles = new Vector3(0, 0, 0);
				}
			}


			if (damaged && !isDead)
			{
				damageTimer += Time.deltaTime;
				if (damageTimer >= damageTime)
				{
					damaged = false;
					damageTimer = 0;
				}
			}

			walkTimer += Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		if (!boboTreino)
		{

			if (!isDead && !stop)
			{
				Vector3 targetDitance = target.position - transform.position;
				float hForce = targetDitance.x / Mathf.Abs(targetDitance.x);

				if (walkTimer >= UnityEngine.Random.Range(1f, 2f))
				{
					zForce = UnityEngine.Random.Range(-1, 2);
					walkTimer = 0;
				}

				if (Mathf.Abs(targetDitance.x) < 1.5f)
				{
					hForce = 0;
				}

				if (!damaged)
					rb.velocity = new Vector3(hForce * currentSpeed, 0, zForce * currentSpeed);

				anim.SetFloat("Speed", Mathf.Abs(currentSpeed));

				if (Mathf.Abs(targetDitance.x) < distanceAttackX && Mathf.Abs(targetDitance.z) < distanceAttackZ && Time.time > nextAttack)
				{
					anim.SetTrigger("Attack");
					attack.SetEnemyAttack(enemyHit);
					currentSpeed = 0;
					nextAttack = Time.time + attackRate;
				}
			}

			rb.position = new Vector3
				(
					rb.position.x,
					rb.position.y,
					Mathf.Clamp(rb.position.z, minHeight, maxHeight));
		}
	}

	
    [System.Obsolete]
    public void TookDamage(int damage)
	{
		if (!isDead)
		{
			damaged = true;
			currentHealth -= damage;
			anim.SetTrigger("HitDamage");
			FindObjectOfType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, enemyName, enemyImage); // UIManager singleton
			if(currentHealth <= 0)
			{
				isDead = true;
				rb.AddRelativeForce(new Vector3(3, 5, 0), ForceMode.Impulse);
				PlaySong(deathSound);
			}
		}
	}
	
	public void DisableEnemy()
	{
		if(!boss) Destroy(gameObject);
	}

	void ResetSpeed()
	{
		currentSpeed = maxSpeed;
	}

	public void PlaySong(AudioClip clip)
	{
		audioS.clip = clip;
		if(gameManager.GetSoundFX() && audioS) audioS.Play();
	}

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    public void SpriteDamage(int damage)
    {
        spriteRenderer.color = damageColor;
        Invoke("ReleaseDamage", damageExibitionTime);
        GameObject newDamageText = Instantiate(damageText, damageTextPosition.position, Quaternion.identity);
        newDamageText.GetComponentInChildren<Text>().text = damage.ToString();
        Destroy(newDamageText, 1);
        ComboManager.instance.SetCombo();
    }

    void ReleaseDamage()
    {
        spriteRenderer.color = defaultColor;
    }

    [Serializable]
    public class EnemyHit
	{
		public int damage = 1;
		public AudioClip collisionSound, deathSound;
    }

    void PlayEnemyHit(EnemyHit enemyHit)
    {
        attack.SetEnemyAttack(enemyHit);
    }

    public void SetStop()
    {
        if (stop)
        {
            stop = false;
        }
        else
        {
            stop = true;
        }
    }

	public int GetHealth()
	{
		return currentHealth;
	}

	public void ShakeCam()
	{
		cam.Shake();
	}

	public bool GetIsDead()
	{
		return isDead;

    }

}
