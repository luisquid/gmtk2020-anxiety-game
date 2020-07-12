using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource sfx;
    public AudioClip[] audios;

    private void Awake()
    {
        instance = this;
    }

    public void Click()
    {
        sfx.PlayOneShot(audios[0], PlayerPrefs.GetFloat("volume",1));
    }

    public void NewChirp()
    {
        sfx.PlayOneShot(audios[1], PlayerPrefs.GetFloat("volume", 1));
    }

    public void NewKey()
    {
        sfx.PlayOneShot(audios[2], PlayerPrefs.GetFloat("volume", 1));
    }

    public void KeyCompleted()
    {
        sfx.PlayOneShot(audios[3], PlayerPrefs.GetFloat("volume", 1));
    }

    public void KeyExploded()
    {
        sfx.PlayOneShot(audios[4], PlayerPrefs.GetFloat("volume", 1));
    }
}