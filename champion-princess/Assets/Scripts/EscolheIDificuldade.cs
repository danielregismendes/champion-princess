using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EscolheDificuldade : MonoBehaviour
{
    public Text textDificuldade;
    public LocalizationData localizationData;

    private int dif = 0;
    private List<String> dificuldades = new List<String>();
    private GameManager gameManager;
    private AudioSource audioSource;


    [System.Obsolete]
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();

        textDificuldade.text = gameManager.GetDif();

        dificuldades.Add("FACIL");
        dificuldades.Add("NORMAL");
        dificuldades.Add("DIFICIL");
        dificuldades.Add("SUKEBAN!");

        for(int i=0; i < dificuldades.Count; i++)
        {
            if (dificuldades[i] == gameManager.GetLingua())
            {
                dif = i;
            }
        }

    }

    private void Update()
    {
        if (textDificuldade.text != GetText(dif + 6)) textDificuldade.text = GetText(dif + 6);
    }

    public void DifMais()
    {
        if (gameManager.GetSoundFX() && audioSource) audioSource.Play();

        if (dif == 3)
        {
            dif = 0;
        }
        else 
        {
            dif++;
        }
        
        gameManager.SetDif(dificuldades[dif]);
        textDificuldade.text = GetText(dif + 6);

    }
    public void DifMenos()
    {
        if (gameManager.GetSoundFX() && audioSource) audioSource.Play();

        if (dif == 0)
        {
            dif = 3;
        }
        else
        {
            dif--;
        }

        gameManager.SetDif(dificuldades[dif]);
        textDificuldade.text = GetText(dif + 6);

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

