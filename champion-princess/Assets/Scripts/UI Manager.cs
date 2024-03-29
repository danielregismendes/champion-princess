using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour

{
    public Slider healthUI;
    public Image playerImage;
    public Text playerName;
    public Text livesText;

    public GameObject enemyUI;
    public GameObject playerUI;
    public Slider enemySlider;
    public Text enemyName;
    public Image enemyImage;

    public GameObject gameOverUI;

    public float enemyUITime = 4f;

    private float enemyTimer;
    private Player player;
    private Animator anim;


    [System.Obsolete]
    // Start is called before the first frame update
    void Start()
    {

        player = FindObjectOfType<Player>();
        healthUI.maxValue = player.maxHealth;
        healthUI.value = healthUI.maxValue;
        playerName.text = player.playerName;
        playerImage.sprite = player.playerImage;
        anim = GetComponent<Animator>();
        UpdateLives();

    }

    // Update is called once per frame
    void Update()
    {
        enemyTimer += Time.deltaTime;
        if (enemyTimer >= enemyUITime)
        {
            enemyUI.SetActive(false);
            enemyTimer = 0;
        }
    }

    public void UpdateHealt(int amount)
    {
        healthUI.value = amount;
    }

    public void UpdateEnemyUI(int maxHealth, int currentHealth, string name, Sprite image)
    {
        enemySlider.maxValue = maxHealth;
        enemySlider.value = currentHealth;
        enemyName.text = name;
        enemyImage.sprite = image;
        enemyTimer = 0;
        enemyUI.SetActive(true);
    }

    [System.Obsolete]   
    public void UpdateLives()
    {
        if(FindObjectOfType<GameManager>().GetLives()>=0) livesText.text = "x " + FindObjectOfType<GameManager>().GetLives().ToString();
    }

    [System.Obsolete]
    public void GameOver()
    {
        playerUI.SetActive(false);
        enemyUI.SetActive(false);
        anim.SetTrigger("Game Over");
        StartCoroutine("Esperar");
    }

    [System.Obsolete]
    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(5);
        FindObjectOfType<GameManager>().GameOver();
        SceneManager.LoadScene(1);
    }

}
