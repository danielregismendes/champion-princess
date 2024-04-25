using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControler : MonoBehaviour
{
    public AudioClip levelSong, bossSong, levelClearSong;

    private AudioSource audioS;//não economiza letra no nome da variável, não tem impacto positivo
    private GameManager gameManager;

    [Obsolete]
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>(); // ver comentário sobre padrão singleton
        PlaySong(levelSong);
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetVolume(float volume)
    {
        if (audioS)  audioS.volume = volume;
    }


    public void PlaySong(AudioClip clip)
    {

        audioS.clip = clip;
        if (gameManager.GetMusic() && audioS) audioS.Play();

    }

    public void StopSong() { audioS.Stop(); }

}
