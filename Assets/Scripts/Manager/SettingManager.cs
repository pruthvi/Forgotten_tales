using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    // BGM
    public float BGMVolume
    {
        get
        {
            return GameManager.Instance.AudioManager.BGMChannel.volume;
        }

        set
        {
            if (value >= 0 && value <= 1)
            {
                GameManager.Instance.AudioManager.BGMChannel.volume = value;
            }
        }
    }
    public float BGMVolumeAdjustmentRatio = 0.1f;
    public float MaxBGMVolumeValue = 1;
    public float MinBGMVolumeValue = 0;

    // SFX
    public float SFXVolume
    {
        get
        {
            return GameManager.Instance.AudioManager.SFX1Channel.volume;
        }

        set
        {
            if (value >= 0 && value <= 1)
            {
                GameManager.Instance.AudioManager.SFX1Channel.volume = value;
            }
        }
    }
    public float SFXVolumeAdjustmentRatio = 0.1f;
    public float MaxSFXVolumeValue = 1;
    public float MinSFXVolumeValue = 0;

    // Narrator
    public float NarratorVolume
    {
        get
        {
            return GameManager.Instance.AudioManager.NarratorChannel.volume;
        }

        set
        {
            if (value >= 0 && value <= 1)
            {
                GameManager.Instance.AudioManager.NarratorChannel.volume = value;
            }
        }
    }
    public float NarratorVolumeAdjustmentRatio = 0.1f;
    public float MaxNarratorVolume = 1;
    public float MinNarratorVolume = 0;

    public float NarratorSpeed
    {
        get
        {
            return GameManager.Instance.AudioManager.NarratorChannel.pitch;
        }

        set
        {
            if (value >= -3 && value <= 3)
            {
                GameManager.Instance.AudioManager.NarratorChannel.pitch = value;
            }
        }
    }
    public float NarratorSpeedAdjustmentRatio = 0.25f;
    public float MaxNarratorSpeedValue = 1.75f;
    public float MinNarratorSpeedValue = 1;
}
