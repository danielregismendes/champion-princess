using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCombo : MonoBehaviour
{

    public Combo[] combos;
    public Attack attack;
    public List<string> currentCombo;
    public UnityEvent OnStartCombo, OnFinishCombo;
    public bool weapon = false;


    private Animator anim;
    private bool startCombo;
    private Hit currentHit, nextHit;
    private float comboTimer;
    private bool canHit = true;
    private bool resetCombo;
    private bool onGround;
    private Transform groundCheck;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {

        groundCheck = gameObject.transform.Find("GroundCheck");


    }

    void Update()
    {

        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        weapon = GetComponentInParent<Player>().GetHoldingWeapon();

        if (onGround && !weapon)
        {

            CheckInputs();

        }
    }

    void CheckInputs()
    {

        if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")) && !canHit)
        {
            resetCombo = true;
        }

        for (int i = 0; i < combos.Length; i++)
        {
            if (combos[i].hits.Length > currentCombo.Count)
            {
                if (Input.GetButtonDown(combos[i].hits[currentCombo.Count].inputButton))
                {
                    if (currentCombo.Count == 0)
                    {
                        OnStartCombo.Invoke();
                        Debug.Log("Primeiro hit foi adicionado");
                        PlayHit(combos[i].hits[currentCombo.Count]);
                        break;
                    }
                    else
                    {
                        bool comboMatch = false;
                        for (int y = 0; y < currentCombo.Count; y++)
                        {
                            if (currentCombo[y] != combos[i].hits[y].inputButton)
                            {
                                Debug.Log("Input não pertence ao hit atual");
                                comboMatch = false;
                                break;
                            }
                            else
                            {
                                comboMatch = true;
                            }
                        }

                        if (comboMatch && canHit)
                        {
                            Debug.Log("Hit adicionado ao combo");
                            nextHit = combos[i].hits[currentCombo.Count];
                            canHit = false;
                            break;
                        }
                    }

                }
            }


        }

        if (startCombo)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer >= currentHit.animationTime && !canHit)
            {
                PlayHit(nextHit);
                if (resetCombo)
                {
                    canHit = false;
                    CancelInvoke();
                    Invoke("ResetCombo", currentHit.animationTime);
                }
            }

            if (comboTimer >= currentHit.resetTime)
            {
                ResetCombo();
            }

        }

    }

    void PlayHit(Hit hit)
    {
        comboTimer = 0;
        attack.SetAttack(hit);
        anim.Play(hit.animation);
        startCombo = true;
        currentCombo.Add(hit.inputButton);
        currentHit = hit;
        canHit = true;
    }

    void ResetCombo()
    {
        resetCombo = false;
        OnFinishCombo.Invoke();
        startCombo = false;
        comboTimer = 0;
        currentCombo.Clear();
        anim.Rebind();
        canHit = true;
    }
}