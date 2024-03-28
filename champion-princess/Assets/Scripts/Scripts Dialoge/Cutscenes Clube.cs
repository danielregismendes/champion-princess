using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DialogeData;

public class CutscenesClube : MonoBehaviour
{

    public DialogeData dialogo1;
    public DialogeData dialogo2;
    public DialogeData dialogo3;

    public DialogeSystem dialogeSystem;
    public DialogeUI dialogeUI;

    private bool repDialogo1 = false;
    private bool repDialogo2 = false;
    private bool repDialogo3 = false;

    public STATEUI state;

    private int fase;
    private STAGEFASE stage;

    private GameManager gameManager;
    private Animator anim;

    [Obsolete]
    private void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        stage = gameManager.GetStage();


        switch (stage)
        {
            case STAGEFASE.FASE0:
                anim.SetTrigger("D1");
                break;

            case STAGEFASE.FASE1:
                anim.SetTrigger("D2");
                break;

            case STAGEFASE.FASE2:
                anim.SetTrigger("D3");
                break;
        }

    }

    private void Update()
    {
        if(dialogeUI) state = dialogeUI.GetStateUI();

        if (repDialogo1 && state == STATEUI.DISABLED)
        {
            gameManager.SetStage(STAGEFASE.FASE1);
            NextFase();
        }

        if (repDialogo2 && state == STATEUI.DISABLED)
        {
            gameManager.SetStage(STAGEFASE.FASE2);
            NextFase();
        }

        if (repDialogo3 && state == STATEUI.DISABLED)
        {
            gameManager.SetStage(STAGEFASE.FASE3);
            NextFase();
        }

    }

    [Obsolete]
    public void Dialogo1()
    {
        dialogeSystem = FindObjectOfType<DialogeSystem>();
        dialogeUI = FindObjectOfType<DialogeUI>();
        dialogeSystem.SetDialogo(dialogo1);
        dialogeSystem.Next();
        repDialogo1= true;

    }

    [Obsolete]
    public void Dialogo2() 
    {
        dialogeSystem = FindObjectOfType<DialogeSystem>();
        dialogeUI = FindObjectOfType<DialogeUI>();
        dialogeSystem.SetDialogo(dialogo2);
        dialogeSystem.Next();
        repDialogo2 = true;
    }

    [Obsolete]
    public void Dialogo3() 
    {
        dialogeSystem = FindObjectOfType<DialogeSystem>();
        dialogeUI = FindObjectOfType<DialogeUI>();
        dialogeSystem.SetDialogo(dialogo3);
        dialogeSystem.Next();
        repDialogo3 = true;
    }
     
    void NextFase()
    {
        stage = gameManager.GetStage();

        switch(stage)
        {
            case STAGEFASE.FASE1:
                fase = 4;
                break;

            case STAGEFASE.FASE2:
                fase = 5;
                break;

            case STAGEFASE.FASE3:
                fase = 6;
                break;
        }

       StartCoroutine(LoadScene(fase));

    }

    IEnumerator LoadScene(int fase)
    {
        anim.SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(fase);
    }

}

