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
    private Transform transform;

    private void Start()
    {
        anim = GetComponent<Animator>();
        transform = GetComponent<Transform>();
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
        Instantiate(dropItem, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }


}
