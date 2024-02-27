using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CrashItem : MonoBehaviour
{
    private int currentDamage = 0;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TookDamage(int damage)
    {
        if (currentDamage<3)
        {
            currentDamage = currentDamage+damage;
            anim.SetInteger("Damage", currentDamage);
        }
    }

    public void DestroyItem()
    {
        gameObject.SetActive(false);
    }
}
