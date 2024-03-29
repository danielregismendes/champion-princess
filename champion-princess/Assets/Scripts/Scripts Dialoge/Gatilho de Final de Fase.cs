using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DialogeData;

public class gatilhoFinalFase : MonoBehaviour
{
    public DialogeData dialogeData;
    public DialogeSystem dialogeSystem;
    public bool eventoColisao;
    public Animator anim;
    public MusicControler musicControler;

    public DialogeUI dialogeUI;

    public bool reproduzido = false;

    public STATEUI state;

    private int fase;
    public STAGEFASE stage;

    public GameManager gameManager;

    [Obsolete]
    private void Start()
    {
        dialogeSystem = FindObjectOfType<DialogeSystem>();
        gameManager = FindObjectOfType<GameManager>();
        dialogeUI = FindObjectOfType<DialogeUI>();
        musicControler = FindObjectOfType<MusicControler>();

        stage = gameManager.GetStage();
    }

    private void Update()
    {
        if (dialogeUI) state = dialogeUI.GetStateUI();
    }

    [Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if(!reproduzido && eventoColisao) 
        {        
            if(other.CompareTag("Player"))
            {
                dialogeSystem = FindObjectOfType<DialogeSystem>();
                dialogeSystem.SetDialogo(dialogeData);
                GetComponent<BoxCollider>().enabled = false;
                dialogeSystem.Next();
                reproduzido = true;
            }
        }


        if (stage == STAGEFASE.FASE1 && reproduzido && state == STATEUI.DISABLED)
        {
            gameManager.SetStage(STAGEFASE.FASE2);
            NextFase();
        }
        else
        {
            if (stage == STAGEFASE.FASE2 && reproduzido && state == STATEUI.DISABLED)
            {
                gameManager.SetStage(STAGEFASE.FASE3);
                NextFase();
            }
        }

    }

    void NextFase()
    {
        stage = gameManager.GetStage();

        switch (stage)
        {
            case STAGEFASE.FASE0:
                fase = 3;
                break;
            case STAGEFASE.FASE1:
                fase = 3;
                break;

            case STAGEFASE.FASE2:
                fase = 3;
                break;

            case STAGEFASE.FASE3:
                fase = 3;
                break;
        }

        StartCoroutine(LoadScene(fase));

    }

    IEnumerator LoadScene(int fase)
    {
        anim.SetTrigger("Fade");
        musicControler.SetVolume(1f);
        musicControler.PlaySong(musicControler.levelClearSong);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(fase);
    }

}
