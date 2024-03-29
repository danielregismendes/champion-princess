using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {

	private AudioSource audioSource;
	private GameManager gameManager;

	[Obsolete]
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		gameManager = FindObjectOfType<GameManager>();

    }

	public void PlaySound(AudioClip clip)
	{
		audioSource.clip = clip;
        if (gameManager.GetSoundFX() && audioSource) audioSource.Play();
	}

}
