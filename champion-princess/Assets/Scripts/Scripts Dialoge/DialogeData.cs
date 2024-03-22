using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "DialogeData", menuName="ScriptableObject/TalkScript",order=1)]
public class DialogeData : ScriptableObject
{

    public string sheetId, gridId;
    
    public List<Dialogue> items;

    [System.Obsolete]
    [ContextMenu("Sync")]
    private void Sync()
    {

        ReadGoogleSheets.FillData<Dialogue>(sheetId, gridId, list =>
        {
            items = list;
            ReadGoogleSheets.SetDirty(this);

        });


    }

    [Obsolete]
    [ContextMenu("OpenSheet")]
    private void Open()
    {
        ReadGoogleSheets.OpenUrl(sheetId, gridId);
    }

    [Serializable]
    public class Dialogue
    {
        public Sprite avatar;
        public Sprite nomeImg;
        public string nome;
        public string textoPT;
        public string textoEN;
        public string textoES;
        public string textoFR;
        public string textoDE;
        public string textoIT;
        public string textoRU;
        public string textoZH;
        public string textoHI;
        public string textoJA;
    }

}
