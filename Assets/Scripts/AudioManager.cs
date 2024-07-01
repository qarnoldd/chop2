using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        PlayMusic("combat");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log(s + "  not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.volume = s.volume;
            musicSource.Play(); 
        }
    }
    public void PlaySFX(string name, AudioSource source)
    {
        if (source == null)
        {
            Debug.Log("Source not Found");
        }
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            {
                Debug.Log(s + " not found");
            }
        }   
        else { 
            source.volume = s.volume;
            source.PlayOneShot(s.clip);
        }
    }
}
