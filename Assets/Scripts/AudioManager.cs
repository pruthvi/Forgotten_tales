using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour{

    public AudioSource BackgroundSource;
    public AudioSource SfxSource;
    public AudioSource NarrativeSource;

    public int FastForwardModifier{ get; private set; }

    void Awake()
    {
        FastForwardModifier = 1;
    }

    public void FastForwardNarrative()
    {
        if (NarrativeSource.clip == null)
        {
            return;
        }
        if(FastForwardModifier + 1 > 4)
        {
            FastForwardModifier = 1;
        }
        else
        {
            FastForwardModifier++;
        }

        NarrativeSource.pitch = 1 + (0.25f * (FastForwardModifier));
    }
}
