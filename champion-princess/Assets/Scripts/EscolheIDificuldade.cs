using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EscolheDificuldade : MonoBehaviour
{
    public Text textDificuldade;

    private int dif = 0;
    private List<String> dificuldades = new List<String>();
    private GameManager gameManager;


    [System.Obsolete]
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

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

    public void DifMais()
    {
        if (dif == 3)
        {
            dif = 0;
        }
        else 
        {
            dif++;
        }
        
        gameManager.SetDif(dificuldades[dif]);
        textDificuldade.text = dificuldades[dif];

    }
    public void DifMenos()
    {

        if (dif == 0)
        {
            dif = 3;
        }
        else
        {
            dif--;
        }

        gameManager.SetDif(dificuldades[dif]);
        textDificuldade.text = dificuldades[dif];

    }

}
