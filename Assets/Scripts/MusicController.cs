using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    public bool playOnStart = false;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
        if(playOnStart)
        {
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void ChoseMusicFromArray(int audioClip)
    {
        audioSource.clip = audioClips[audioClip];
    }

    public void ChoseMusicFromSource(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
    }
}
