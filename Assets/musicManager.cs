using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;

    public int selectSong = 0;
    public float PLAY_VOLUME = 0.1f;

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            playMusic();
            selectSong++;
        }
        
    }

    public void playMusic()
    {
            switch(selectSong)
            {
                case 0:
                    audioSource.PlayOneShot(clip1, PLAY_VOLUME);
                    break;
                case 1:
                    audioSource.PlayOneShot(clip2, PLAY_VOLUME);
                    break;
                case 2:
                    audioSource.PlayOneShot(clip3, PLAY_VOLUME);
                    break;
                case 3:
                    audioSource.PlayOneShot(clip4, PLAY_VOLUME);
                    break;
                case 4:
                    audioSource.PlayOneShot(clip5, PLAY_VOLUME);
                    break;
                case 5:
                    audioSource.PlayOneShot(clip6, PLAY_VOLUME);
                    break;
            }
        }
}



