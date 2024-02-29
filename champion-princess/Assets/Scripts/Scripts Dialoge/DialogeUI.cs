using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogeUI : MonoBehaviour
{
    Image background;
    Image personagem;
    TextMeshProUGUI nameText;
    TextMeshProUGUI talkText;

    public float speed = 10f;
    bool open = false;

    private void Awake()
    {
        personagem = transform.GetChild(0).GetComponent<Image>();
        background = transform.GetChild(1).GetComponent<Image>();
        talkText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        nameText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    void Update()
    {

        if (open)
        {
            background.fillAmount = Mathf.Lerp(background.fillAmount,1,speed*Time.deltaTime);
            personagem.fillAmount = Mathf.Lerp(background.fillAmount, 1, speed * Time.deltaTime);
        }
        else
        {
            background.fillAmount = Mathf.Lerp(background.fillAmount, 0, speed * Time.deltaTime);
            personagem.fillAmount = Mathf.Lerp(background.fillAmount, 0, speed * Time.deltaTime);
        }
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetImage(Sprite imagem)
    {
        personagem.sprite = imagem;
    }

    public void Enable()
    {
        background.fillAmount = 0;
        personagem.fillAmount = 0;
        open = true;
    }

    public void Disable()
    {
        open = false;
        nameText.text = "";
        talkText.text = "";
    }

}
