using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private bool press = false;
    private Animator anim;
    private AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!press)
        {
            if (Input.anyKeyDown)
            {
                if(anim){ anim.SetTrigger("Press Button");}
                else { LoadScene(); };
                if (audioSource) { audioSource.Play(); };
                press = true;
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}
