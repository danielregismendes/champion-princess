using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public enum STATE
{
    DISABLED,
    WAITING,
    TYPING
}

public class DialogeSystem : MonoBehaviour
{
    public DialogeData dialogeData;

    int currentText = 0;
    bool finished = false;

    TypeTextAnimation typeText;
    DialogeUI dialogeUI;

    Player player;
    Enemy enemy;
    Pause pause;
    bool playerStop;
    bool enemyStop;
    bool paused;


    STATE state;

    public string lingua = "PORTUGUES";

    GameManager gameManager;

    [Obsolete]
    private void Awake()
    {
        typeText = FindObjectOfType<TypeTextAnimation>();
        dialogeUI = FindObjectOfType<DialogeUI>();
        gameManager = FindObjectOfType<GameManager>();

        if(gameManager) lingua = gameManager.GetLingua();

        typeText.TypeFinished = OnTypeFinishe;

    }


    void Start()
    {

        state = STATE.DISABLED;

    }


    [Obsolete]
    void Update()
    {
        pause = FindObjectOfType<Pause>();

        if(pause) paused = pause.GetPause();

        if (state == STATE.DISABLED) return;

        switch (state)
        {
            case STATE.WAITING:
                Waiting();
                break;
            case STATE.TYPING:
                Typing();
                break;
        }
        
    }

    [Obsolete]
    public void Next()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
        if(player) playerStop = player.GetStop();
        if (enemy) enemyStop = player.GetStop();

        if (!playerStop && !enemyStop)
        {
            if (player) player.SetStop();
            if (enemy) enemy.SetStop();
        }

        if (currentText == 0)
        {
            dialogeUI.Enable();
        }

        dialogeUI.SetName(dialogeData.items[currentText].nome);
        dialogeUI.SetImage(dialogeData.items[currentText].avatar, dialogeData.items[currentText].nomeImg);

        switch (lingua)
        {
            case "PORTUGUES":
                typeText.fullText = dialogeData.items[currentText++].textoPT;
                break;
            case "INGLES":
                typeText.fullText = dialogeData.items[currentText++].textoEN;
                break;
            case "ESPANHOL":
                typeText.fullText = dialogeData.items[currentText++].textoES;
                break;
            case "FRANCES":
                typeText.fullText = dialogeData.items[currentText++].textoFR;
                break;
            case "ALEMAO":
                typeText.fullText = dialogeData.items[currentText++].textoDE;
                break;
            case "ITALIANO":
                typeText.fullText = dialogeData.items[currentText++].textoIT;
                break;
            case "RUSSO":
                typeText.fullText = dialogeData.items[currentText++].textoRU;
                break;
            case "CHINES":
                typeText.fullText = dialogeData.items[currentText++].textoZH;
                break;  
            case "HINDI":
                typeText.fullText = dialogeData.items[currentText++].textoHI;
                break;
            case "JAPONES":
                typeText.fullText = dialogeData.items[currentText++].textoJA;
                break;
        }
        
        if (currentText == dialogeData.items.Count) finished = true;

        typeText.StartTyping();
        state = STATE.TYPING;
    }

    [Obsolete]
    void OnTypeFinishe()
    {
        state = STATE.WAITING;
    }

    [Obsolete]
    void Waiting()
    {
        if (Input.GetButtonDown("Fire1") && !paused)
        {
            if (!finished)
            {
                Next();
            }
            else
            {
                if (player) player.SetStop();
                if (enemy) enemy.SetStop();
                dialogeUI.Disable();
                state = STATE.DISABLED;
                currentText = 0;
                finished = false;
            }
        }
    }

    void Typing()
    {
        if (Input.GetButtonDown("Fire1") && !paused)
        {
            typeText.Skip();
            state = STATE.WAITING;
        }

    }

    public void SetDialogo(DialogeData dialogo)
    {
        dialogeData = dialogo;
    }

    public STATE GetState()
    {
        return state;
    }

}
