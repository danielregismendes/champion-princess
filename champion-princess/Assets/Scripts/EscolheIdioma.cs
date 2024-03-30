using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EscolheIdioma : MonoBehaviour
{
    public Text textIdioma;
    public LocalizationData localizationData;


    private int lingua = 0;
    private List<String> idiomas = new List<String>();
    private GameManager gameManager;
    private AudioSource audioSource;


    [System.Obsolete]
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();

        textIdioma.text = gameManager.GetLingua();

        idiomas.Add("PORTUGUES");
        idiomas.Add("INGLES");
        idiomas.Add("ESPANHOL");
        idiomas.Add("FRANCES");
        idiomas.Add("ALEMAO");
        idiomas.Add("ITALIANO");
        idiomas.Add("RUSSO");
        idiomas.Add("CHINES");
        idiomas.Add("HINDI");
        idiomas.Add("JAPONES");

        for(int i=0; i < idiomas.Count; i++)
        {
            if (idiomas[i] == gameManager.GetLingua())
            {
                lingua = i;
            }
        }

    }

    public void IdiomaMais()
    {
        if (gameManager.GetSoundFX() && audioSource) audioSource.Play();

        if (lingua == 9)
        {
            lingua = 0;
        }
        else 
        {
            lingua++;
        }
        
        gameManager.SetLingua(idiomas[lingua]);
        textIdioma.text = GetText(lingua + 11);

    }
    public void IdiomaMenos()
    {
        if (gameManager.GetSoundFX() && audioSource) audioSource.Play();

        if (lingua == 0)
        {
            lingua = 9;
        }
        else
        {
            lingua--;
        }

        gameManager.SetLingua(idiomas[lingua]);
        textIdioma.text = GetText(lingua+11);

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
