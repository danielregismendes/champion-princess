using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public DialogeData dialogeDataDerrota;
    public DialogeData dialogeDataVitoria;
    public DialogeSystem dialogeSystem;
    public bool eventoColisao;
    public Animator anim;
    public MusicControler musicControler;

    public UIManager uiManager;
    public DialogeUI dialogeUI;

    public bool reproduzido = false;

    public STATEUI state;

    private Enemy enemy;
    private Player player;
    public STAGEFASE stage;

    public GameManager gameManager;

    public bool boosDead = false;
    public bool playerDead = false;

    private bool winTrigger = false;
    private bool loseTrigger = false;

    [Obsolete]
    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        dialogeSystem = FindObjectOfType<DialogeSystem>();
        gameManager = FindObjectOfType<GameManager>();
        dialogeUI = FindObjectOfType<DialogeUI>();
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();
        musicControler = FindObjectOfType<MusicControler>();

        stage = gameManager.GetStage();
    }

    [Obsolete]
    private void Update()
    {
        if (dialogeUI) state = dialogeUI.GetStateUI();
        if (enemy) boosDead = enemy.GetIsDead();
        if (player && gameManager.GetLives()<0) playerDead = player.GetIsDead();

        if (!reproduzido && playerDead)
        {
            dialogeSystem.SetDialogo(dialogeDataDerrota);
            dialogeSystem.Next();
            if (dialogeUI) state = dialogeUI.GetStateUI();
            reproduzido = true;
        }
        else
        {
            if (!reproduzido && boosDead)
            {
                dialogeSystem.SetDialogo(dialogeDataVitoria);
                dialogeSystem.Next();
                if (dialogeUI) state = dialogeUI.GetStateUI();
                reproduzido = true;
            }
        }

        if (reproduzido && state == STATEUI.DISABLED)
        {
            if(playerDead)
            {

                Derrota();

            }
            else
            {

                Vitoria();

            }
        }

    }

    [Obsolete]
    void Derrota()
    {
        if (!loseTrigger)
        {
            loseTrigger = true;
            musicControler.SetVolume(1f);
            musicControler.PlaySong(musicControler.bossSong);
            uiManager.GameOver();
        }
        
    }

    void Vitoria()
    {
        if (!winTrigger)
        {
            winTrigger = true;
            musicControler.SetVolume(1f);
            musicControler.PlaySong(musicControler.levelClearSong);
            StartCoroutine(LoadScene(7));
        }
        
    }

    IEnumerator LoadScene(int fase)
    {
        anim.SetTrigger("Fade");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(fase);
    }

}
