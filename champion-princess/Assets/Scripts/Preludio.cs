using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Preludio : MonoBehaviour
{
    public GameManager gameManager;
    public DialogeData localizationData;
    public Text textoPreludio;

    private bool press = false;
    private Animator anim;
    private AudioSource audioSource;

    [Obsolete]
    private void Start()
    {

        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        textoPreludio.text = GetText(0);

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

    public string GetText(int indice)
    {
        String result = null;

        switch (gameManager.GetLingua())
        {
            case "PORTUGUES":
                result = localizationData.items[indice].textoPT;
                break;

            case "INGLES":
                result = localizationData.items[indice].textoEN;
                break;

            case "ESPANHOL":
                result = localizationData.items[indice].textoES;
                break;

            case "FRANCES":
                result = localizationData.items[indice].textoFR;
                break;

            case "ALEMAO":
                result = localizationData.items[indice].textoDE;
                break;

            case "ITALIANO":
                result = localizationData.items[indice].textoIT;
                break;

            case "RUSSO":
                result = localizationData.items[indice].textoRU;
                break;

            case "CHINES":
                result = localizationData.items[indice].textoZH;
                break;

            case "HINDI":
                result = localizationData.items[indice].textoHI;
                break;

            case "JAPONES":
                result = localizationData.items[indice].textoJA;
                break;

        }

        return result;

    }

}
