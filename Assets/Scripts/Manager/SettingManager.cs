using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SettingManager
{
    // BGM
    public float BGMVolume
    {
        get
        {
            return _gameManager.AudioManager.BackgroundSource.volume;
        }

        set
        {
            if (value >= 0 && value <= 1)
            {
                _gameManager.AudioManager.BackgroundSource.volume = value;
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
            return _gameManager.AudioManager.SFXSource.volume;
        }

        set
        {
            if (value >= 0 && value <= 1)
            {
                _gameManager.AudioManager.SFXSource.volume = value;
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
            return _gameManager.AudioManager.NarrativeSource.volume;
        }

        set
        {
            if (value >= 0 && value <= 1)
            {
                _gameManager.AudioManager.NarrativeSource.volume = value;
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
            return _gameManager.AudioManager.NarrativeSource.pitch;
        }

        set
        {
            if (value >= -3 && value <= 3)
            {
                _gameManager.AudioManager.NarrativeSource.pitch = value;
            }
        }
    }
    public float NarratorSpeedAdjustmentRatio = 0.25f;
    public float MaxNarratorSpeedValue = 1.75f;
    public float MinNarratorSpeedValue = 1;

    private GameManager _gameManager;

    public SettingManager(GameManager gm)
    {
        _gameManager = gm;
    }
}
