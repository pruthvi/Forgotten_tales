using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum SelectionState { None, Selecting, Completed }
public enum DialogueState { Dialogue, DialogueOption }
public class InGameState : GameState
{

    private string textInstruction = "[R] - Replay | [T] - Replay Dialogue\n[F] Fast Forward | [Esc] Skip | ";

    private Narrator _narrator;

    private GameEvent _currentEvent;

    public GameEvent CurrentGameEvent;

    public DialogueState DialogueState;

    public InGameState(GameManager gm) : base(gm)
    {
        _narrator = gm.Narrator;
    }

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.InGame;
        }
    }

    public override void UpdateGUI()
    {
        _gameManager.TextUI.text = textInstruction + "Speed: x" + _gameManager.Narrator.Speed;
        if (DialogueState == DialogueState.Dialogue)
        {
            _gameManager.TextDescription.text = ((Dialogue)_currentEvent).TextDescription;
        }
        else if(DialogueState == DialogueState.DialogueOption)
        {
            updateDialogueWithOptionGUI();
        }
    }

    public override void OnStateEnter()
    {
        _narrator.CurrentAct.Init();
        _currentEvent = _narrator.CurrentAct.CurrentDialogue;
        Debug.Log("Current Act" + _currentEvent.name);
        _gameManager.InputManager.ChangeInputLayer(InputLayer.Dialogue, _narrator.CurrentAct.CurrentDialogue.Options.Count);
        UpdateGUI();
        _gameManager.TextDescription.alignment = TextAnchor.UpperLeft;
        _gameManager.TextUI.alignment = TextAnchor.UpperLeft;
        _gameManager.GameProgress = GameProgress.DialogueBegin;
        _gameManager.AudioManager.PlayBGM(_gameManager.AudioManager.BGMNarrative);
        _narrator.Stop();
        _gameManager.InputManager.ResetSelection();
    }

    public void updateDialogueWithOptionGUI()
    {
        string options = "";
        for (int i = 0; i < _narrator.CurrentAct.CurrentDialogue.Options.Count; i++)
        {
            options += (_gameManager.InputManager.SelectedItemIndex == i ? "> " : "\t") + " Option " + (i + 1) + ": \n\t\t" + _narrator.CurrentAct.CurrentDialogue.Options[i].TextDescription + "\n";
        }
        _gameManager.TextDescription.text = _narrator.CurrentAct.CurrentDialogue.TextDescription + "\n" + options;
        if (_gameManager.InputManager.SelectedItemIndex < _narrator.CurrentAct.CurrentDialogue.Options.Count)
        {
            _narrator.Play(_narrator.CurrentAct.CurrentDialogue.Options[_gameManager.InputManager.SelectedItemIndex].AudioDescription, PlayType.Option);
        }
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        switch (DialogueState)
        {
            case DialogueState.Dialogue:
                onDialogue();
                break;
            case DialogueState.DialogueOption:
                onDialogueOption();
                break;
        }
    }

    private void onDialogueOption()
    {

    }

    private void onDialogue()
    {
        // If no dialogue playing, play the current dialogue
        if (_narrator.Status == NarratorStatus.Idle)
        {
            // Play the current dialogue
            _gameManager.Narrator.Play(_narrator.CurrentAct.CurrentDialogue.AudioDescription);
        }
        if (_narrator.Status == NarratorStatus.Completed || _narrator.Status == NarratorStatus.Skipped)
        {
            // If dialogue completed or skipped
            // if has options
            _gameManager.InputManager.ResetSelection();
            if (_currentEvent.GameEventType == GameEventType.Dialogue)
            {
                Dialogue dialogue = (Dialogue)_currentEvent;
                if (dialogue.HasOptions)
                {
                    _gameManager.InputManager.ChangeInputLayer(InputLayer.ChooseDialogueOption, dialogue.Options.Count);
                }
                else
                {
                    
                }
                
            }
        }
    }

    private void onHasOptions()
    {
        DialogueState = DialogueState.DialogueOption;
        _gameManager.InputManager.ChangeInputLayer(InputLayer.ChooseDialogueOption, _narrator.CurrentAct.CurrentDialogue.Options.Count);
        _gameManager.TextDescription.text = _narrator.CurrentAct.CurrentDialogue.TextDescription;
        updateDialogueWithOptionGUI();
    }

    private void goNextEventBaseOnSelection()
    {

    }

    private void goNextDefaultEvent()
    {

    }

    public void NextEvent()
    {
        if (_currentEvent.GameEventType == GameEventType.Dialogue)
        {
            Dialogue dialogue = (Dialogue)_currentEvent;

            if (dialogue.HasOptions)
            {
                goNextEventBaseOnSelection();
            }
            else
            {
                if (_currentEvent.DefaultEvent != null)
                {
                    _currentEvent = _currentEvent.DefaultEvent;
                }
                else
                {
                    if (dialogue.IsEndOfAct)
                    {
                        Debug.Log("End of act");
                    }
                    else
                    {
                        Debug.LogError("No default Event or Option");
                    }
                }
            }
        }
    }
}