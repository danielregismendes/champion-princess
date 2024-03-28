using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject options;
    public Pause menuPause;
    public GameManager gameManager;

    [System.Obsolete]
    private void Start()
    {
        menuPause = GetComponent<Pause>();

        gameManager = FindObjectOfType<GameManager>();

    }

    public void NewGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Options()
    {
        if(options.activeSelf)
        {
            options.SetActive(false);
        }
        else
        {
            options.SetActive(true);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        if (menuPause.GetPause())
        {
            Pause();
        }
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        menuPause.Pausar();
    }

    public void Musica()
    {
        gameManager.SetMusic();
    }

    public void Audio()
    {
        gameManager.SetSoundFX();
    }

}
