using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum STAGEFASE
{
    FASE0,
    FASE1,
    FASE2,
    FASE3,
}

public class GameManager : MonoBehaviour
{
    public int lives;

    private string dificuldade = "FACIL"; //Evitar hard-code de strings. Tem uma quantidade limitada de opções, bom use case de enum
    private string lingua = "PORTUGUES"; //Idem a linha anterior
    private GameManager gameManager; //Singleton pattern: public static GameManager gameManager
    private int currentLives;
    private bool music = true;
    private bool soundFX = true;
    public STAGEFASE stage;
    public int maxHP = 10;

    void Awake()
    {
        SetDif(dificuldade);
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

    public void SetDif(string dif)
    {
        dificuldade = dif;

        switch (dificuldade)
        {
            case "FACIL":
                lives = 3;
                currentLives = 3;
                maxHP = 10;
                break;

            case "NORMAL":
                lives = 1;
                currentLives = 1;
                maxHP = 10;
                break;

            case "DIFICIL":
                lives = 0;
                currentLives = 0;
                maxHP = 10;
                break;

            case "SUKEBAN!":
                lives = 0;
                currentLives = 0;
                maxHP = 1;
                break;
        }
    }

    public int GetMaxHP()
    {
        return maxHP;
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
        SetStage(STAGEFASE.FASE1);
    }

    public void SetLingua(string Lingua)
    {
        lingua = Lingua;
    }

    public string GetLingua()
    {
        return lingua;
    }

    public string GetDif()
    {
        return dificuldade;
    }

    [Obsolete]
    public void SetMusic()
    {
        if(music)
        {
            music = false;
            MusicControler musicControler = FindObjectOfType<MusicControler>();
            musicControler.StopSong();
        }
        else
        {
            music = true;
            MusicControler musicControler = FindObjectOfType<MusicControler>();
            musicControler.PlaySong(musicControler.levelSong);
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

    public void SetStage(STAGEFASE newStage)
    {
        stage = newStage;
    }

    public STAGEFASE GetStage()
    {
        return stage;
    }

}
