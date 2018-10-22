using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialougeStatus { BeginDialogue, PlayingDialogue, DialogueOption, EndDialogue }
[CreateAssetMenu(menuName ="Forgotten Tale/Game Event/Dialogue", order = 2)]
public class Dialogue : GameEvent {

    public bool IsEndOfAct = false;

    [TextArea]
    public string DialogueText;
    public AudioClip DialogueClip;

    public List<Option> Options = new List<Option>();

    public AudioClip[] SFX;
    public float[] SFXTime;

    public bool StopDialogueSFX;

    private DialougeStatus dialougeStatus;

    public bool HasOptions
    {
        get
        {
            return Options.Count > 0;
        }
    }

    public override GameEventType GameEventType
    {
        get
        {
            return GameEventType.Dialogue;
        }
    }

    public override void OnEnter()
    {
        _gm = GameManager.Instance;

        StopDialogueSFX = false;
        PlayDialogueSFXBasedOnTime(SFX, SFXTime);

        // Play the dialogue clip
        _gm.Narrator.Play(DialogueClip, PlayType.Dialogue);
        _gm.UIManager.Content = DialogueText;

        _gm.InputManager.SetMenuItemLimit(Options.Count);

        dialougeStatus = DialougeStatus.BeginDialogue;
    }

    public override void OnExit()
    {
        StopDialogueSFX = true;
        _gm.Narrator.Stop();
    }

    public override void OnUpdate()
    {
        switch (dialougeStatus)
        {
            case DialougeStatus.BeginDialogue:
                dialougeStatus = DialougeStatus.PlayingDialogue;
                break;
            case DialougeStatus.PlayingDialogue:
                onCompletedOrSkip();
                break;
            case DialougeStatus.DialogueOption:
                break;
        }
    }

    public override void OnGUIChange()
    {
        
    }

    private void showDialogueOptions()
    {
        string options = "";
        for (int i = 0; i < Options.Count; i++)
        {
            options += (_gm.InputManager.SelectedItemIndex == i ? "> " : "\t") + " Option " + (i + 1) + ": \n\t\t" + Options[i].TextDescription + "\n";
        }
        _gm.UIManager.Content = DialogueText + "\n" + options;
    }

    private void onCompletedOrSkip()
    {
        if (_gm.Narrator.CompletedOrSkipped)
        {
            if (IsEndOfAct)
            {
                dialougeStatus = DialougeStatus.EndDialogue;
                _gm.ChangeState(GameStateType.MainMenu);
                return;
            }
            if (HasOptions)
            {
                showDialogueOptions();
                _gm.Narrator.Play(Options[_gm.InputManager.SelectedItemIndex].AudioDescription);
                dialougeStatus = DialougeStatus.DialogueOption;
            }
            else
            {
                _gm.InGameState.NextEvent();
                dialougeStatus = DialougeStatus.EndDialogue;
            }
        }
    }

    public override void OnInput()
    {
        switch (dialougeStatus)
        {
            case DialougeStatus.PlayingDialogue:
                if (_gm.InputManager.SelectionSkipOrExit(true))
                {
                    _gm.Narrator.Skip();
                    onCompletedOrSkip();
                }
                break;
            case DialougeStatus.DialogueOption:
                if (_gm.InputManager.SelectionUp())
                {
                    showDialogueOptions();
                }
                if (_gm.InputManager.SelectionDown())
                {
                    showDialogueOptions();
                }
                if (_gm.InputManager.SelectionConfirm())
                {
                    dialougeStatus = DialougeStatus.EndDialogue;
                    _gm.InGameState.NextEvent(Options[_gm.InputManager.SelectedItemIndex].NextEvent);
                }
                break;
        }
    }

    public void PlayDialogueSFXBasedOnTime(AudioClip[] clips, float[] time)
    {
        _gm.StartCoroutine(playDialogueSFXBasedOnTime(clips, time));
    }

    IEnumerator playDialogueSFXBasedOnTime(AudioClip[] clips, float[] time)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            // Break the loop if list doesn't match
            if (clips.Length != time.Length)
            {
                break;
            }
            // 
            float timeBetween = i == 0 ? time[i] : (i == clips.Length - 1 ? 0 : time[i] - time[i + 1]);
            yield return new WaitForSeconds(timeBetween * (1 / _gm.Narrator.Speed));
            // Play the clip on giving channel
            _gm.AudioManager.Play(clips[i], AudioChannel.SFX2);
            // Break the loop if this should be cancel
            if (!StopDialogueSFX)
            {
                break;
            }
        }
    }

    public override void Init()
    {
    }
}
