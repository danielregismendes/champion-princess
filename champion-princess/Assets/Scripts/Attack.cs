using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

[System.Serializable]
public class Attack : MonoBehaviour
{

    public AudioPlayer audioPlayer;
    public int weaponDamage;

    private int damage;
    private int enemyDamage;
    private bool slowDown;
    private AudioClip hitSound;
    private AudioClip hitSoundEnemy;
    private bool weapon;


    public void SetAttack(Hit hit)
    {

        damage = hit.damage;
        slowDown = hit.slowDown;
        hitSound = hit.hitSound;
    }

    public void SetEnemyAttack(EnemyHit enemy)
    {
        enemyDamage = enemy.damage;
        hitSoundEnemy = enemy.collisionSound;
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        Player player = other.GetComponent<Player>();
        CrashItem crashItem = other.GetComponent<CrashItem>();


        if (enemy != null)
        {
            if (enemy.GetHealth() > 0)
            {
                weapon = GetComponentInParent<Player>().GetHoldingWeapon();

                if (weapon)
                {
                    damage = weaponDamage;
                }

                enemy.SpriteDamage(damage);
                enemy.TookDamage(damage);
                audioPlayer.PlaySound(hitSound);
                if (slowDown)
                    SlowDown.instance.SetSlowDown();
            }
        }

        if (player != null)
        {
            Debug.Log("Dano do inimigo = "+ enemyDamage);
            player.TookDamage(enemyDamage);
            audioPlayer.PlaySound(hitSoundEnemy);
        }

        if (crashItem != null)
        {
            damage = 1;
            Debug.Log("Dano no barril = " + damage);
            crashItem.TookDamage(damage);
            audioPlayer.PlaySound(hitSound);

        }
     }
}
