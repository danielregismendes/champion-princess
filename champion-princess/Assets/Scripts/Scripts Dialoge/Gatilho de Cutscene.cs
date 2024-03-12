using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gatilhoCutscene : MonoBehaviour
{
    DialogeSystem dialogeSystem;
    bool reproduzido = false;

    [Obsolete]
    private void Awake()
    {
        dialogeSystem = FindObjectOfType<DialogeSystem>();
    }

    [Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if(!reproduzido) 
        {        
            if(other.CompareTag("Player"))
            {
                GetComponent<BoxCollider>().enabled = false;
                dialogeSystem.Next();
                reproduzido = true;
            }
        }
    }

}
