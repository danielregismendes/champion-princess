using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "LocalizationData", menuName="ScriptableObject/LocalizationData",order=1)]
public class LocalizationData : ScriptableObject
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
        public string label;
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
