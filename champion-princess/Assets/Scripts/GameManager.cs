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

    private string lingua = "PORTUGUES";
    private GameManager gameManager;
    private int currentLives;
    private bool music = true;
    private bool soundFX = true;
    public STAGEFASE stage;

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

    public void SetStage(STAGEFASE newStage)
    {
        stage = newStage;
    }

    public STAGEFASE GetStage()
    {
        return stage;
    }

}
