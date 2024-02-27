using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Weapon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color color;
    private int durability;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ActicateWeapon(Sprite sprite, Color color, int durabilityValue, int damage)
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;
        durability = durabilityValue;
        GetComponent<Attack>().weaponDamage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {

        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            durability--;
            if (durability < 0)
            {
                spriteRenderer.sprite = null;
                GetComponentInParent<Player>().SetHoldingWeaponToFalse();
            }
        }

    }

}