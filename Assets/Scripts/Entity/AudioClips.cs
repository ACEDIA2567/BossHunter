using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class AudioClips : MonoBehaviour
{
    public AudioClip[] clips;

    public void PlayBGM(SFXClip clip)
    {
        SoundManager.Instance.PlaySound(clips[(int)clip]);
    }

    public void PlaySFX(SFXClip clip)
    {
        SoundManager.Instance.PlayShot(clips[(int)clip]);
    }
}
