using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public enum SFXClip
{
    Attack1,
    Attack2,
    parrying,
    Die
}

public enum BGMClip
{
    Intro,
    InGame,
    Fight,
    End
}

public class SoundManager : Singleton<SoundManager>
{
    public AudioMixer mixer;

    public AudioClip[] clipBGM;
    private AudioSource sourceSFX;
    private AudioSource sourceBGM;

    protected override void Awake()
    {
        base.Awake();
        AudioInit();
    }

    private void AudioInit()
    {
        GameObject bgm = Instantiate(Resources.Load<GameObject>("Sound/BGMSource"), transform);
        GameObject sfx = Instantiate(Resources.Load<GameObject>("Sound/SFXSource"), transform);
        clipBGM = bgm.GetComponent<AudioClips>().clips;
        sourceBGM = bgm.GetComponent<AudioSource>();
        sourceSFX = sfx.GetComponent<AudioSource>();
    }

    public void SetMasterVolum(float sliderValue)
    {
        mixer.SetFloat("MusicVol" ,Mathf.Log10(sliderValue) * 20);
    }

    public void MasterVolumMute(bool toggle)
    {
        AudioListener.volume = toggle ? 0 : 1;
    }

    public void BGMVolumMute(bool toggle)
    {
        sourceBGM.mute = toggle;
    }

    public void SFXVolumMute(bool toggle)
    {
        sourceSFX.mute = toggle;
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