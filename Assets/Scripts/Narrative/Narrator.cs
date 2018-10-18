using UnityEngine;

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

    public Narrator(AudioSource source)
    {
        _source = source;
        SpeedModifier = 1;
        SetToIdle();
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

    public void SetToIdle()
    {
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

        _source.pitch = 1 + (SpeedModifier == 1 ? 0 : (0.25f * (SpeedModifier - 1)));
    }
}