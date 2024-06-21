using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SFXClip
{
    Attack,
    Die,
}

public enum BGMClip
{
    Start,
    Fight,
    End
}

public class SoundManager : Singleton<SoundManager>
{
    public AudioMixer mixer;

    public AudioClip[] clipSFX;
    public AudioClip[] clipBGM;
    private AudioSource sourceMaster;
    private AudioSource sourceSFX;
    private AudioSource sourceBGM;

    private void Awake()
    {
        AudioInit();
        sourceMaster = GetComponent<AudioSource>();
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

    public void MasterVolumMute(Toggle toggle)
    {
        sourceMaster.mute = toggle.isOn;
        foreach (AudioSource source in transform.GetComponentsInChildren<AudioSource>())
        {
            source.mute = toggle.isOn;
        }
    }

    public void BGMVolumMute(Toggle toggle)
    {
        if (sourceMaster.mute) return;
        sourceBGM.mute = toggle.isOn;
    }

    public void SFXVolumMute(Toggle toggle)
    {
        if (sourceMaster.mute) return;
        sourceSFX.mute = toggle.isOn;
    }

    public void SetBGMVolume(float sliderValue)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }

    public void PlaySound(AudioClip clip)
    {
        sourceBGM.Stop();
        sourceBGM.clip = clip;
        sourceBGM.Play();
    }



    public void PlayShot(AudioClip clip)
    {
        sourceSFX.PlayOneShot(clip);
    }
}
