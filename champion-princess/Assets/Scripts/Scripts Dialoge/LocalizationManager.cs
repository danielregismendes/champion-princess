using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    public LocalizationData localizationData;

    public TextList[] uiList;

    private GameManager gameManager;


    [Obsolete]
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // GameManager sendo um singleton, é acessível via GameManager.gameManager e não precisa armazenar referências em outras classes. FindObjectOfType e métodos semelhantes são pesados e devem ser evitados quando possível.
        
    }

    
    void Update()
    {

        for (int i = 0; i < uiList.Length; i++)
        {

            
                if (uiList[i].textObjects[0].texto)
                {
                    if (uiList[i].textObjects[0].texto.text != GetText(i)) uiList[i].textObjects[0].texto.text = GetText(i);
                }
                if (uiList[i].textObjects[0].textoPro)
                {
                    if (uiList[i].textObjects[0].textoPro.text != GetText(i)) uiList[i].textObjects[0].textoPro.text = GetText(i);
                }

        }

        

    }

    public string GetText(int indice)
    {
        String result = null;

        switch (gameManager.GetLingua())
        {
            case "PORTUGUES":
                result = localizationData.items[indice].textoPT;
                break;
                
            case "INGLES":
                result = localizationData.items[indice].textoEN;
                break;

            case "ESPANHOL":
                result = localizationData.items[indice].textoES;
                break;

            case "FRANCES":
                result = localizationData.items[indice].textoFR;
                break;

            case "ALEMAO":
                result = localizationData.items[indice].textoDE;
                break;

            case "ITALIANO":
                result = localizationData.items[indice].textoIT;
                break;

            case "RUSSO":
                result = localizationData.items[indice].textoRU;
                break;

            case "CHINES":
                result = localizationData.items[indice].textoZH;
                break;

            case "HINDI":
                result = localizationData.items[indice].textoHI;
                break;

            case "JAPONES":
                result = localizationData.items[indice].textoJA;
                break;

        }

        return result;

    }
   
}

[Serializable]
public class TextList
{
    public TextObjects[] textObjects;
}

[Serializable]
public class TextObjects
{
    public Text texto;
    public TextMeshProUGUI textoPro;
}
