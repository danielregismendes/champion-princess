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

    STATE state;

    [Obsolete]
    private void Awake()
    {
        typeText = FindObjectOfType<TypeTextAnimation>();
        dialogeUI = FindObjectOfType<DialogeUI>();

        typeText.TypeFinished = OnTypeFinishe;
    }


    void Start()
    {

        state = STATE.DISABLED;

    }


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

    public void Next()
    {
        if(currentText == 0)
        {
            dialogeUI.Enable();
        }

        dialogeUI.SetName(dialogeData.talkScript[currentText].name);
        dialogeUI.SetImage(dialogeData.talkScript[currentText].dialogeImage);

        typeText.fullText = dialogeData.talkScript[currentText++].text;

        if (currentText == dialogeData.talkScript.Count) finished = true;

        typeText.StartTyping();
        state = STATE.TYPING;
    }

    void OnTypeFinishe()
    {
        state = STATE.WAITING;
    }

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

}
