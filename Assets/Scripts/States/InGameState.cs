using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum SelectionState { None, Selecting, Completed }
public enum DialogueState { BeginDialogue, PlayingDialogue, PlayingDialogueOption }
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

        if (_currentEvent.GameEventType == GameEventType.Dialogue)
        {
            if (DialogueState == DialogueState.PlayingDialogue)
            {
                _gameManager.TextDescription.text = ((Dialogue)_currentEvent).TextDescription;
            }
            else
            {
                updateDialogueWithOptionGUI();
            }
        }
        

        //if (DialogueState == DialogueState.PlayingDialogue)
        //{
        //    _gameManager.TextDescription.text = ((Dialogue)_currentEvent).TextDescription;
        //}
        //else if (DialogueState == DialogueState.PlayingDialogueOption)
        //{
        //    updateDialogueWithOptionGUI();
        //}
    }

    public override void OnStateEnter()
    {

        _narrator.Stop();
        _gameManager.InputManager.ChangeInputLayer(InputLayer.Dialogue, _narrator.CurrentAct.CurrentDialogue.Options.Count);
        _gameManager.TextDescription.alignment = TextAnchor.UpperLeft;
        _gameManager.TextUI.alignment = TextAnchor.UpperLeft;
        _gameManager.AudioManager.PlayBGM(_gameManager.AudioManager.BGMNarrative);
        _gameManager.InputManager.ResetSelection();
        DialogueState = DialogueState.BeginDialogue;
        UpdateGUI();
    }

    private void updateDialogueWithOptionGUI()
    {
        if (_currentEvent.GameEventType == GameEventType.Dialogue)
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
        
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        if (_currentEvent.GameEventType == GameEventType.Battle)
        {
            if (((Battle)_currentEvent).BattleResult == BattleResult.Win)
            {
                _currentEvent = ((Battle)_currentEvent).WinEvent;
            }
            else if (((Battle)_currentEvent).BattleResult == BattleResult.Lose)
            {
                _currentEvent = ((Battle)_currentEvent).LoseEvent;
            }
        }
        if (_currentEvent.DefaultEvent != null && _currentEvent.DefaultEvent.GameEventType == GameEventType.Battle)
        {
            if (_narrator.CompletedOrSkipped)
            {
                _currentEvent = _currentEvent.DefaultEvent;
                _gameManager.ChangeState(GameStateType.Battle);
                _gameManager.InputManager.ChangeInputLayer(InputLayer.ChoosePreCombatOption, 0);
            }
        }

        switch (DialogueState)
        {
            case DialogueState.BeginDialogue:
                onDialogueBegin();
                break;
            case DialogueState.PlayingDialogue:
                onPlayingDialogue();
                break;
            case DialogueState.PlayingDialogueOption:
                onPlayingDialogueOption();
                break;
        }
    }

    private void onDialogueBegin()
    {
        if (_currentEvent.GameEventType == GameEventType.Dialogue)
        {
            Dialogue d = (Dialogue)_currentEvent;
            _narrator.Play(d.AudioDescription, PlayType.Dialogue);
            _gameManager.TextDescription.text = ((Dialogue)_currentEvent).TextDescription;
            _gameManager.InputManager.ChangeInputLayer(InputLayer.Dialogue, 0);
            DialogueState = DialogueState.PlayingDialogue;
        }
        else
        {
            _gameManager.ChangeState(GameStateType.Battle);
        }
    }

    private void onPlayingDialogueOption()
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

    private void onPlayingDialogue()
    {
        if (_currentEvent.GameEventType == GameEventType.Dialogue)
        {
            Dialogue d = (Dialogue)_currentEvent;
            // Check if player skip if dialogue completed or skipped
            if (_narrator.CompletedOrSkipped)
            {
                // If current dialogue has option
                if (d.HasOptions)
                {
                    _gameManager.InputManager.ChangeInputLayer(InputLayer.ChooseDialogueOption, d.Options.Count);
                    DialogueState = DialogueState.PlayingDialogueOption;
                    updateDialogueWithOptionGUI();
                }
                else
                {
                    if (d.IsEndOfAct)
                    {
                        Debug.Log("End of act " + d.name);
                        _gameManager.ChangeState(GameStateType.MainMenu);
                    }
                    else
                    {
                        if (d.DefaultEvent != null)
                        {
                            if (d.DefaultEvent.GameEventType == GameEventType.Battle)
                            {
                                _gameManager.ChangeState(GameStateType.Battle);
                            }
                            else
                            {
                                _currentEvent = d.DefaultEvent;
                            }
                        }
                        else
                        {
                            Debug.LogError(d.name + " has no option or default event");
                        }
                    }
                    
                }
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
                    if (ge.GameEventType == GameEventType.Dialogue)
                    {
                        _gameManager.InputManager.ChangeInputLayer(InputLayer.Dialogue, 0);
                        DialogueState = DialogueState.BeginDialogue;
                    }
                }
                else
                {
                    Debug.LogError("Option " + dialogue.Options[_gameManager.InputManager.SelectedItemIndex].name + " does not have next event. ");
                }
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