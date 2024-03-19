using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lives;
    public string lingua = "PT";

    private GameManager gameManager;
    private int currentLives;


    // Start is called before the first frame update
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
    }

    public void SetLingua(string abreviacaoLingua)
    {
        lingua = abreviacaoLingua;
    }
}
