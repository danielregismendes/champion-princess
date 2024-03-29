using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject options;
    public Pause menuPause;
    public GameManager gameManager;

    private AudioSource audioSource;

    [System.Obsolete]
    private void Start()
    {
        menuPause = GetComponent<Pause>();

        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();

    }

    public void NewGame()
    {
        if (gameManager.GetSoundFX() && audioSource) audioSource.Play();

        SceneManager.LoadScene(2);
    }

    public void Options()
    {
        if (gameManager.GetSoundFX() && audioSource) audioSource.Play();

        if (options.activeSelf)
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
        if (gameManager.GetSoundFX() && audioSource) audioSource.Play();

        Application.Quit();
    }

    public void Menu()
    {
        if (gameManager.GetSoundFX() && audioSource) audioSource.Play();

        if (menuPause.GetPause())
        {
            Pause();
        }
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        if (gameManager.GetSoundFX() && audioSource) audioSource.Play();

        menuPause.Pausar();
    }

    public void Musica()
    {
        if (gameManager.GetSoundFX() && audioSource) audioSource.Play();

        gameManager.SetMusic();
    }

    public void Audio()
    {
        if (gameManager.GetSoundFX() && audioSource) audioSource.Play();

        gameManager.SetSoundFX();
    }

}
