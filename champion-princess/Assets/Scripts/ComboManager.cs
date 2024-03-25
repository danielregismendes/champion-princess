using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour {

	public static ComboManager instance;

	public GameObject combo;

	public TextMeshProUGUI comboText;

	public TextMeshProUGUI comboLabel;

	public float resetTime = 2f;

	private Animator comboTextAnimator;

	private int totalCombo;

    private string comboLabelAtual;

    private void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {

		comboTextAnimator = combo.GetComponent<Animator>();

	}
	
	public void SetCombo()
	{
		totalCombo++;
        comboLabelAtual = comboLabel.text;

        if (totalCombo < 10) 
		{
			if(comboLabelAtual != "GOOD")  comboLabel.text = "GOOD";
        }
        if(totalCombo >= 10 && totalCombo < 20)
        {
            if (comboLabelAtual != "BRUTAL!") comboLabel.text = "BRUTAL!";
        }
        if (totalCombo >= 20 && totalCombo < 30)
        {
            if (comboLabelAtual != "EPIC!") comboLabel.text = "EPIC!";
        }
        if (totalCombo >= 30)
        {
            if (comboLabelAtual != "SUKEBAN!") comboLabel.text = "SUKEBAN!";
        }

        comboText.text = totalCombo.ToString();
		comboTextAnimator.SetTrigger("Hit");

		CancelInvoke();
		Invoke("ResetCombo", 2f);
	}

	void ResetCombo()
	{
		totalCombo = 0;
	}
}
