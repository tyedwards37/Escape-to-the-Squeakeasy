using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AudioInterface : MonoBehaviour
{
    public AudioClip MainTheme, StoryTheme, CollectKey, OpenDoor, WinSound, LoseSound;
    private AudioSource musicSource;
    private AudioSource sfxSource;
    public static AudioInterface Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();
            musicSource.volume = 0.20f;
            sfxSource.volume = 1.0f;
        }
    }

    public void PlayMainTheme()
    {
        musicSource.clip = MainTheme;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayStoryTheme()
    {
        musicSource.clip = StoryTheme;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopThemeMusic()
    {
        musicSource.loop = false;
        musicSource.Stop();
    }

    public void PlayCollectKey()
    {
        sfxSource.PlayOneShot(CollectKey);
    }

    public void PlayOpenDoor()
    {
        sfxSource.PlayOneShot(OpenDoor);
    }

    public void PlayWinSound()
    {
        sfxSource.PlayOneShot(WinSound);
        sfxSource.PlayOneShot(WinSound);
        sfxSource.PlayOneShot(WinSound);
        sfxSource.PlayOneShot(WinSound);
        sfxSource.PlayOneShot(WinSound);
        sfxSource.PlayOneShot(WinSound);
    }
    
    public void PlayLoseSound()
    {
        sfxSource.PlayOneShot(LoseSound);
    }

    public bool IsCurrentlyPlayingMusic()
    {
        return musicSource.isPlaying;
    }
}