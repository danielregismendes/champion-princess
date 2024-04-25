using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Pause : MonoBehaviour // essa classe inteira poderia ser um bool no GameManager.
{
    public GameObject menuPausa;

    private bool paused = false;

    void Update()
    {

        if (Input.GetButtonDown("Cancel")) //evitar processamento de input em update, utilizar eventos. Ver InputSystem em packages.
        {
            Pausar();
        }
    }

    public void Pausar()
    {
        if (Time.timeScale == 1)
        {
            paused = true;
            Time.timeScale = 0;
            menuPausa.SetActive(true);

        }
        else
        {
            paused = false;
            Time.timeScale = 1;
            menuPausa.SetActive(false);
        }

    }

   public bool GetPause()
    {
        return paused;
    }

}
