using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gatilhoCutscene : MonoBehaviour
{
    DialogeSystem dialogeSystem;
    bool reprodizido = false;

    [Obsolete]
    private void Awake()
    {
        dialogeSystem = FindObjectOfType<DialogeSystem>();
    }

    [Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if(!reprodizido) 
        {        
            if(other.CompareTag("Player"))
            {
                GetComponent<BoxCollider>().enabled = false;
                dialogeSystem.Next();
                reprodizido = true;
            }
        }
    }

}
