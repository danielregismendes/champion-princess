using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EscolheIdioma : MonoBehaviour
{
    public Text textIdioma;

    private int lingua = 0;
    private List<String> idiomas = new List<String>();
    private GameManager gameManager;


    [System.Obsolete]
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        textIdioma.text = gameManager.GetLingua();

        idiomas.Add("PORTUGES");
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
        if (lingua == 9)
        {
            lingua = 0;
        }
        else 
        {
            lingua++;
        }
        
        gameManager.SetLingua(idiomas[lingua]);
        textIdioma.text = idiomas[lingua];

    }
    public void IdiomaMenos()
    {

        if (lingua == 0)
        {
            lingua = 9;
        }
        else
        {
            lingua--;
        }

        gameManager.SetLingua(idiomas[lingua]);
        textIdioma.text = idiomas[lingua];

    }

}
