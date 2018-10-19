using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour{

    public AudioSource BackgroundSource;
    public AudioSource SFXSource;
    public AudioSource NarrativeSource;

    // SFX
    public AudioClip SFXMenuItemSelection;
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
        SFXSource.clip = clip;
        SFXSource.Play();
    }
}
