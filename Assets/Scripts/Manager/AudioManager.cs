using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioChannel { BGM, SFX1, SFX2, Narrator }
public class AudioManager : MonoBehaviour
{

    public AudioSource BGMChannel;
    public AudioSource SFX1Channel;
    public AudioSource SFX2Channel;
    public AudioSource NarratorChannel;

    // BGM
    public AudioClip BGMMainMenuToIntro;
    public AudioClip BGMNarrative;
    public AudioClip BGMCombat;

    void Start()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 4)
        {
            BGMChannel = sources[0];
            SFX1Channel = sources[1];
            SFX2Channel = sources[2];
            NarratorChannel = sources[3];
        }
    }

    //
    public bool LoopAudio;

    public void Play(AudioClip clip, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.BGM:
                BGMChannel.clip = clip;
                BGMChannel.Play();
                break;
            case AudioChannel.SFX1:
                SFX1Channel.clip = clip;
                SFX1Channel.Play();
                break;
            case AudioChannel.SFX2:
                SFX2Channel.clip = clip;
                SFX2Channel.Play();
                break;
            case AudioChannel.Narrator:
                NarratorChannel.clip = clip;
                NarratorChannel.Play();
                break;
        }
    }
}
