using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscolheIdioma : MonoBehaviour
{
    private string lingua;
    GameManager gameManager;

    public void Idioma()
    {
       
        lingua = gameObject.name;
        gameManager = FindObjectOfType<GameManager>();

        gameManager.SetLingua(lingua);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
