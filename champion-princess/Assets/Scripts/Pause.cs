using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject menuPausa;

    private bool paused = false;

     void Update()
    {

        if (Input.GetButtonDown("Cancel"))
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
