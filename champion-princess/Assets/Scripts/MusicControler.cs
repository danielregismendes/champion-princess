using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControler : MonoBehaviour
{
    public AudioClip levelSong, bossSong, levelClearSong;

    private AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        PlaySong(levelSong);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySong(AudioClip clip)
    {

        audioS.clip = clip;
        audioS.Play();

    }

}
