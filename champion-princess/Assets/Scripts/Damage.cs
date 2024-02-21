using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{

    public Color damageColor;
    public float damageTime = 0.1f;

    public GameObject damageText;
    public Transform damageTextPosition;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    public void TakeDamage(int damage)
    {
        spriteRenderer.color = damageColor;
        Invoke("ReleaseDamage", damageTime);
        GameObject newDamageText = Instantiate(damageText, damageTextPosition.position, Quaternion.identity);
        newDamageText.GetComponentInChildren<Text>().text = damage.ToString();
        Destroy(newDamageText, 1);
        ComboManager.instance.SetCombo();
    }

    void ReleaseDamage()
    {
        spriteRenderer.color = defaultColor;
    }
}