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

    public GameEvent CurrentGameEvent
    {
        get
        {
            return _currentEvent;
        }

        set
        {
            _currentEvent = value;
        }
    }

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
        Dialogue d = (Dialogue)_currentEvent;
        string options = "";
        for (int i = 0; i < d.Options.Count; i++)
        {
            options += (_gameManager.InputManager.SelectedItemIndex == i ? "> " : "\t") + " Option " + (i + 1) + ": \n\t\t" + d.Options[i].TextDescription + "\n";
        }
        _gameManager.TextDescription.text = d.TextDescription + "\n" + options;
        if (_gameManager.InputManager.SelectedItemIndex < d.Options.Count)
        {
            _narrator.Play(d.Options[_gameManager.InputManager.SelectedItemIndex].AudioDescription, PlayType.Option);
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
        Dialogue d = (Dialogue)_currentEvent;
        // If no dialogue option playing, play the current dialogue option
        if (_narrator.Status == NarratorStatus.Idle)
        {
            // Play the current dialogue
            if (d.HasOptions)
            {
                _gameManager.Narrator.Play(d.Options[_gameManager.InputManager.SelectedItemIndex].AudioDescription);
            }
        }
    }

    private void onDialogue()
    {
        Dialogue d = (Dialogue)_currentEvent;
        // If no dialogue playing, play the current dialogue
        if (_narrator.Status == NarratorStatus.Idle)
        {
            // Play the current dialogue
            _gameManager.Narrator.Play(d.AudioDescription);
            DialogueState = DialogueState.Dialogue;
            UpdateGUI();

        }
        // If dialogue completed or skipped
        if (_narrator.Status == NarratorStatus.Completed || _narrator.Status == NarratorStatus.Skipped)
        {
            // Reset input in case
            _gameManager.InputManager.ResetSelection();
            Debug.Log("Skipped");
            // If current event is dialogue;
            if (d.HasOptions)
            {
                Debug.Log("Has Options");
                _gameManager.InputManager.ChangeInputLayer(InputLayer.ChooseDialogueOption, d.Options.Count);
                DialogueState = DialogueState.DialogueOption;
                _gameManager.TextDescription.text = _narrator.CurrentAct.CurrentDialogue.TextDescription;
                UpdateGUI();
            }
            else
            {
                Debug.Log("No Options");
            }
        }
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
                GameEvent ge = dialogue.Options[_gameManager.InputManager.SelectedItemIndex].NextEvent;
                if (ge != null)
                {
                    _currentEvent = ge;
                    UpdateGUI();
                    _narrator.Stop();
                }
                else
                {
                    Debug.LogError("Option " + dialogue.Options[_gameManager.InputManager.SelectedItemIndex].name + " does not have next event. ");
                }
            }
            else
            {
                if (dialogue.DefaultEvent != null)
                {
                    if (dialogue.DefaultEvent.GameEventType == GameEventType.Battle)
                    {
                        _gameManager.ChangeState(GameStateType.Battle);
                    }
                    else
                    {
                        _currentEvent = _currentEvent.DefaultEvent;
                        UpdateGUI();
                    }
                }
                //if (dialogue.IsEndOfAct)
                //{
                //    Debug.Log("End of act, Return to Main Menu");
                //    //Return to main menu;
                //    _gameManager.ChangeState(GameStateType.MainMenu);
                //}
                //else
                //{
                //    if (_currentEvent.DefaultEvent == null)
                //    {
                //        Debug.LogError("No default Event or Option");
                //    }
                //    else
                //    {
                //        
                //        // If the new event is Battle
                //        if (_currentEvent.GameEventType == GameEventType.Battle)
                //        {
                //            // Start Battle
                //            
                //        }
                //    }
                //}
            }
        }
    }

    public bool OnDialogue
    {

        get
        {
            if (_currentEvent == null || _currentEvent.GameEventType != GameEventType.Dialogue)
            {
                return false;
            }
            return true;
        }
    }
}