using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    private GameManager gameManager;


    [Obsolete]
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        /*
        if (!press)
        {
            if (Input.anyKeyDown)
            {
                if (audioSource) { audioSource.Play(); };
                press = true;
                StartCoroutine(LoadScene(1));
            }
        }
        */
    }

    public void LoadScene()
    {
        if(gameManager) gameManager.GameOver();
        if (gameManager) gameManager.SetStage(STAGEFASE.FASE0);
        SceneManager.LoadScene(1);
    }

}
