using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour {

	public static SlowDown instance;

	public float slowDownTime = 1f;

	private bool canSlowDown;
	private float timer;

	private void Awake()
	{
		instance = this;
	}

	// Update is called once per frame
	void Update () {

		if (canSlowDown)
		{
			timer += Time.unscaledDeltaTime;
			Time.timeScale += Time.unscaledDeltaTime / slowDownTime;
			if(timer >= slowDownTime)
			{
				canSlowDown = false;
				Time.timeScale = 1;
			}
		}

	}

	public void SetSlowDown()
	{
		Time.timeScale = 0;
		canSlowDown = true;
		timer = 0;
	}
}
