using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    public AudioMixer mixer;

    public AudioClip[] clipSFX;
    public AudioClip[] clipBGM;
    public AudioSource sourceMaster;
    private AudioSource sourceSFX;
    private AudioSource sourceBGM;

    private void Awake()
    {
        AudioInit();
    }

    private void AudioInit()
    {
        GameObject bgm = Instantiate(Resources.Load<GameObject>("Sound/BGMSource"), transform);
        GameObject sfx = Instantiate(Resources.Load<GameObject>("Sound/SFXSource"), transform);
        clipBGM = bgm.GetComponent<AudioClips>().clips;
        clipSFX = bgm.GetComponent<AudioClips>().clips;
        sourceBGM = bgm.GetComponent<AudioSource>();
        sourceSFX = sfx.GetComponent<AudioSource>();
    }

    public void SetMasterVolum(float sliderValue)
    {
        mixer.SetFloat("MusicVol" ,Mathf.Log10(sliderValue) * 20);
    }

    public void MasterVolumMut(Toggle toggle)
    {
        sourceMaster.mute = toggle;
    }

    public void BGMVolumMut(Toggle toggle)
    {
        sourceSFX.mute = toggle;
    }

    public void SFXVolumMut(Toggle toggle)
    {
        sourceBGM.mute = toggle;
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
        for (int i = 0; i < clipBGM.Length; i++)
        {
            if (name == clipBGM[i].name)
            {
                sourceBGM.Stop();
                sourceBGM.clip = clipBGM[i];
                sourceBGM.Play();
            }
        }
    }



    public void PlayShot(string name)
    {
        for (int i = 0; i < clipSFX.Length; i++)
        {
            if (name == clipSFX[i].name)
            {
                sourceSFX.PlayOneShot(clipSFX[i]);
            }
        }
    }
}
