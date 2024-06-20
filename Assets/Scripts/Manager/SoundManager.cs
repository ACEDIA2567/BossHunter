using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    public AudioMixer mixer;

    public AudioClip[] clips;
    public AudioSource AudioSource;

    public void SetMasterVolum(float sliderValue)
    {
        mixer.SetFloat("MusicVol" ,Mathf.Log10(sliderValue) * 20);
    }

    public void SetBGMVolume(float sliderValue)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
    }
    public void SetSFXVolume(float sliderValue)
    {
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }

    public void PlaySound(string name)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (name == clips[i].name)
            {
                AudioSource.clip = clips[i];
                AudioSource.Play();
            }
        }
    }
}
