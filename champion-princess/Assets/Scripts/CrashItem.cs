using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CrashItem : MonoBehaviour
{
    public GameObject dropItem;

    private int currentDamage = 0;
    private Animator anim;
    private Transform trans;

    private void Start()
    {
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
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
        Instantiate(dropItem, trans.position, trans.rotation);
        gameObject.SetActive(false);
    }


}
