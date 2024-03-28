using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum STATEGAME
{
    MENU,
    CUTSCENE,
    TRANSISAO,
    GAMEPLAY
}

public class GameManager : MonoBehaviour
{
    public int lives;

    private string lingua = "PORTUGUES";
    private GameManager gameManager;
    private int currentLives;
    private bool music = true;
    private bool soundFX = true;
    STATEGAME stage;

    void Awake()
    {
        currentLives = lives;

        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        stage = STATEGAME.MENU;
    }

    private void Update()
    {
        
        switch(stage)
        {
            case STATEGAME.MENU:

                break;
            case STATEGAME.CUTSCENE:

                break;
            case STATEGAME.TRANSISAO:

                break;
            case STATEGAME.GAMEPLAY:

                break;
        }  

    }

    public int GetLives()
    {
        return currentLives;
    }

    public void SetLives()
    {
        currentLives--;
    }

    public void GameOver()
    {
        currentLives = lives;
    }

    public void SetLingua(string Lingua)
    {
        lingua = Lingua;
    }

    public string GetLingua()
    {
        return lingua;
    }

    public void SetMusic()
    {
        if(music)
        {
            music = false;
        }
        else
        {
            music = true;
        }
    }

    public bool GetMusic()
    {
        return music;
    }

    public void SetSoundFX()
    {
        if (soundFX)
        {
            soundFX = false;
        }
        else
        {
            soundFX = true;
        }
    }

    public bool GetSoundFX()
    {
        return soundFX;
    }




}
