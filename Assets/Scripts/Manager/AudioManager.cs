using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour{

    public AudioSource BackgroundSource;
    public AudioSource SfxSource;
    public AudioSource NarrativeSource;

    // SFX
    public AudioClip SFXMenuSelection;
    public AudioClip SFXConfirm;
    public AudioClip SFXSkip;

    // BGM
    public AudioClip BGMMainMenuToIntro;
    public AudioClip BGMNarrative;
    public AudioClip BGMCombat;

    public void PlayBGM(AudioClip clip)
    {
        BackgroundSource.clip = clip;
        BackgroundSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SfxSource.clip = clip;
        SfxSource.Play();
    }
}
