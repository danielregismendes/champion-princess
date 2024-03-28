using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Preludio : MonoBehaviour
{
    public GameManager gameManager;
    private bool press = false;
    private Animator anim;
    private AudioSource audioSource;

    [Obsolete]
    private void Start()
    {

        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (!press)
        {
            if (Input.anyKeyDown)
            {
                if (anim) anim.SetTrigger("Fade");
                if (audioSource) audioSource.Play();
                press = true;
            }
        }
    }


    public void NextStage()
    {
        if (anim) anim.SetTrigger("Fade");
        gameManager.SetStage(STAGEFASE.FASE1);
        SceneManager.LoadScene(3);
    }
}
