using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack : MonoBehaviour
{

    public AudioPlayer audioPlayer;

    private int damage;
    private bool slowDown;
    private AudioClip hitSound;

    public void SetAttack(Hit hit)
    {
        damage = hit.damage;
        slowDown = hit.slowDown;
        hitSound = hit.hitSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        Damage enemy = other.GetComponent<Damage>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            audioPlayer.PlaySound(hitSound);
            if (slowDown)
                SlowDown.instance.SetSlowDown();

        }
    }
}
