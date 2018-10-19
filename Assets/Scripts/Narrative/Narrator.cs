﻿using UnityEngine;
using System.Collections;
public enum NarratorStatus { Idle, Speaking, Completed, Skipped }
public enum PlayType { Dialogue, Option }
public class Narrator
{
    private AudioSource _source;
    public NarratorStatus Status { get; private set; }

    public int MaxSpeedModifier = 4;

    public int SpeedModifier { get; private set; }

    private AudioClip _lastDialogue;
    private AudioClip _currentClip;

    private float dialogueBeginTime;

    private GameManager _gameManager;

    public bool ActEnds;

    private Act _currentAct;
    public Act CurrentAct
    {
        get
        {
            return _currentAct;
        }
    }
    private Act[] _acts;
    public Act[] Acts
    {
        get
        {
            return _acts;
        }
    }
    private int _currentActIndex;
    public int CurrentActIndex
    {
        get
        {
            return _currentActIndex;
        }
    }

    public Narrator(GameManager gm, AudioSource source, Act[] acts)
    {
        _gameManager = gm;
        _source = source;
        SpeedModifier = 1;
        Stop();
        _acts = acts;
    }

    public void Begin(int index)
    {
        if (index >= 0 && index < Acts.Length)
        {
            _currentAct = Acts[index];
            _currentActIndex = index;
        }
    }

    public void UpdateStatus()
    {
        if (_source.clip != null)
        {
            if (Status == NarratorStatus.Speaking)
            {
                if (!_source.isPlaying)
                {
                    Status = NarratorStatus.Completed;
                }
            }
        }
    }

    public bool IsIdle
    {
        get
        {
            return Status == NarratorStatus.Idle;
        }
    }

    public bool IsSpeaking
    {
        get
        {
            return Status == NarratorStatus.Speaking;
        }
    }

    public bool Skipped
    {
        get
        {
            return Status == NarratorStatus.Skipped;
        }
    }

    public bool CompletedOrSkipped
    {
        get
        {
            return (Status == NarratorStatus.Completed || Status == NarratorStatus.Skipped);
        }
    }

    public bool CompletedTalking
    {
        get
        {
            return Status == NarratorStatus.Completed;
        }
    }

    public void Play(AudioClip clip, PlayType type)
    {
        if (type == PlayType.Dialogue)
        {
            _lastDialogue = clip;
        }
        Play(clip);
    }

    public void Play(AudioClip clip)
    {
        _currentClip = clip;
        _source.clip = _currentClip;
        _source.Play();
        Status = NarratorStatus.Speaking;
        _gameManager.StartCoroutine(playSFXBasedOnTime());
    }

    public void Replay(PlayType type)
    {
        if(type == PlayType.Dialogue && _lastDialogue != null)
        {
            _source.clip = _lastDialogue;
        }
        else
        {
            _source.clip = _currentClip;
        }
        _source.Play();
        Status = NarratorStatus.Speaking;
    }

    public void Skip()
    {
        Status = NarratorStatus.Skipped;
    }

    public void Stop()
    {
        _source.Stop();
        Status = NarratorStatus.Idle;
    }

    public float Speed
    {
        get
        {
            return _source.pitch;
        }
        set
        {
            if (value >= -3 && value <= 3)
            {
                _source.pitch = value;
                _gameManager.AudioManager.SFXSource.pitch = _source.pitch;
            }
        }
    }

    public void FastForward()
    {
        if (_source.clip == null)
        {
            return;
        }
        if (SpeedModifier + 1 > MaxSpeedModifier)
        {
            SpeedModifier = 1;
        }
        else
        {
            SpeedModifier++;
        }

        _source.pitch = 1 + (SpeedModifier == 1 ? 0 : (_gameManager.SettingManager.NarratorSpeedAdjustmentRatio * (SpeedModifier - 1)));
        _gameManager.AudioManager.SFXSource.pitch = _source.pitch;
    }

    IEnumerator playSFXBasedOnTime()
    {
        for (int i = 0; i < _currentAct.CurrentDialogue.SFX.Count; i++)
        {
            float waitTime = 0;
            if (i < _currentAct.CurrentDialogue.SFX.Count - 1)
            {
                waitTime = (_currentAct.CurrentDialogue.SFXTime[i + 1] - _currentAct.CurrentDialogue.SFXTime[i]);
            }
            _gameManager.AudioManager.PlaySFX(_currentAct.CurrentDialogue.SFX[i]);
            yield return new WaitForSeconds(waitTime * (1 / _gameManager.Narrator.Speed));
        }
    }
}