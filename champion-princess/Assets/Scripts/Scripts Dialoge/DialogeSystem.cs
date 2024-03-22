using System;
using System.Collections;
using System.Collections.Generic;
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
    bool playerStop;
    bool enemyStop;

    STATE state;

    public string lingua;

    GameManager gameManager;

    [Obsolete]
    private void Awake()
    {
        typeText = FindObjectOfType<TypeTextAnimation>();
        dialogeUI = FindObjectOfType<DialogeUI>();
        gameManager = FindObjectOfType<GameManager>();

        lingua = gameManager.lingua;

        typeText.TypeFinished = OnTypeFinishe;

    }


    void Start()
    {

        state = STATE.DISABLED;

    }


    [Obsolete]
    void Update()
    {

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
        playerStop = player.GetStop();
        enemyStop = player.GetStop();

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
            case "PT":
                typeText.fullText = dialogeData.items[currentText++].textoPT;
                break;
            case "EN":
                typeText.fullText = dialogeData.items[currentText++].textoEN;
                break;
            case "ES":
                typeText.fullText = dialogeData.items[currentText++].textoES;
                break;
            case "FR":
                typeText.fullText = dialogeData.items[currentText++].textoFR;
                break;
            case "DE":
                typeText.fullText = dialogeData.items[currentText++].textoDE;
                break;
            case "IT":
                typeText.fullText = dialogeData.items[currentText++].textoIT;
                break;
            case "RU":
                typeText.fullText = dialogeData.items[currentText++].textoRU;
                break;
            case "ZH":
                typeText.fullText = dialogeData.items[currentText++].textoZH;
                break;  
            case "HI":
                typeText.fullText = dialogeData.items[currentText++].textoHI;
                break;
            case "JA":
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
        if (Input.GetButtonDown("Fire1"))
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
        if (Input.GetButtonDown("Fire1"))
        {
            typeText.Skip();
            state = STATE.WAITING;
        }

    }

    public void SetDialogo(DialogeData dialogo)
    {
        dialogeData = dialogo;
    }

}
