using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

[Serializable]
public class Attack : MonoBehaviour
{

    public AudioPlayer audioPlayer;

    private int damage;
    private int enemyDamage;
    private bool slowDown;
    private AudioClip hitSound;
    private AudioClip hitSoundEnemy;

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

        if (enemy != null)
        {
            enemy.SpriteDamage(damage);
            enemy.TookDamage(damage);
            audioPlayer.PlaySound(hitSound);
            if (slowDown)
                SlowDown.instance.SetSlowDown();
        }

        if (player != null)
        {
            Debug.Log("Dano do inimigo = "+ enemyDamage);
            player.TookDamage(enemyDamage);
            audioPlayer.PlaySound(hitSoundEnemy);
        }

    }
}
