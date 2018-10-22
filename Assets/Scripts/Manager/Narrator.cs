using UnityEngine;
using System.Collections;
public enum NarratorStatus { Idle, Speaking, Completed, Skipped }
public enum PlayType { Dialogue, Option }
public class Narrator : MonoBehaviour
{
    private AudioSource _speaker;
    // Narrator

    public NarratorStatus Status;
   
    public int MaxSpeedModifier = 4;

    public int SpeedModifier { get; private set; }

    private AudioClip _lastDialogue;
    private AudioClip _currentClip;

    private float dialogueBeginTime;

    public bool ActEnds;

    private Act _currentAct;
    public Act CurrentAct
    {
        get
        {
            return _currentAct;
        }
    }

    public Act[] Acts;

    public bool StopDialogueSFX;

    public Dialogue FirstDialogue
    {
        get;
        private set;
    }

    void Start()
    {
        _speaker = GameManager.Instance.AudioManager.NarratorChannel;
    }



    // Only call this when change to InGameState
    public void BeginAct(int index)
    {
        if (index >= 0 && index < Acts.Length)
        {
            _currentAct = Acts[index];
            if (_currentAct.Dialogues.Count > 0)
            {
                FirstDialogue = _currentAct.Dialogues[0];
            }
        }
    }

    void Update()
    {
        if (_speaker.clip != null)
        {
            if (Status == NarratorStatus.Speaking)
            {
                if (!_speaker.isPlaying)
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
        _speaker.clip = _currentClip;
        _speaker.Play();
        Status = NarratorStatus.Speaking;
    }

    public void Replay(PlayType type)
    {
        if(type == PlayType.Dialogue && _lastDialogue != null)
        {
            _speaker.clip = _lastDialogue;
        }
        else
        {
            _speaker.clip = _currentClip;
        }
        _speaker.Play();
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

    public void Stop()
    {
        _speaker.Stop();
        Status = NarratorStatus.Idle;
    }

    public float Speed
    {
        get
        {
            return _speaker.pitch;
        }
        set
        {
            if (value >= -3 && value <= 3)
            {
                _speaker.pitch = value;
                GameManager.Instance.AudioManager.SFX1Channel.pitch = _speaker.pitch;
            }
        }
    }

    public void FastForward()
    {
        if (_speaker.clip == null)
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

        _speaker.pitch = 1 + (SpeedModifier == 1 ? 0 : (GameManager.Instance.SettingManager.NarratorSpeedAdjustmentRatio * (SpeedModifier - 1)));
        GameManager.Instance.AudioManager.SFX1Channel.pitch = _speaker.pitch;
    }
}