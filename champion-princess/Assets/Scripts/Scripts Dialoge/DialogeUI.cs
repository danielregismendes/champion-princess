using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum STATEUI
{
    ENABLE,
    DISABLED,
}

public class DialogeUI : MonoBehaviour
{
    Image background;
    Image personagem;
    TextMeshProUGUI nameText;
    TextMeshProUGUI talkText;
    Image nameImage;

    STATEUI stateUI;

    public float speed = 10f;
    bool open = false;

    private void Awake()
    {
        personagem = transform.GetChild(0).GetComponent<Image>();
        background = transform.GetChild(1).GetComponent<Image>();
        talkText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        nameText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        nameImage = transform.GetChild(4).GetComponent<Image>();
    }

    void Update()
    {

        if (open)
        {
            background.fillAmount = Mathf.Lerp(background.fillAmount,1,speed * Time.deltaTime);
            personagem.fillAmount = Mathf.Lerp(background.fillAmount, 1, speed * Time.deltaTime);
            nameImage.fillAmount = Mathf.Lerp(background.fillAmount, 1, speed * Time.deltaTime);

            
        }
        else
        {
            background.fillAmount = Mathf.Lerp(background.fillAmount, 0, speed * Time.deltaTime);
            personagem.fillAmount = Mathf.Lerp(background.fillAmount, 0, speed * Time.deltaTime);
            nameImage.fillAmount = Mathf.Lerp(background.fillAmount, 0, speed * Time.deltaTime);

            stateUI = STATEUI.DISABLED;
        }
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetImage(Sprite personagemImg, Sprite nomeImg)
    {
        personagem.sprite = personagemImg;
        nameImage.sprite = nomeImg;
    }

    public void Enable()
    {
        background.fillAmount = 0;
        personagem.fillAmount = 0;
        nameImage.fillAmount = 0;
        open = true;
        stateUI = STATEUI.ENABLE;
    }

    public void Disable()
    {
        open = false;
        nameText.text = "";
        talkText.text = "";
    }


    public STATEUI GetStateUI()
    {
        return stateUI;
    }

}
