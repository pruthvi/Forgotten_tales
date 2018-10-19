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
        updateDialogueWithOptionGUI();
    }

    public override void OnStateEnter()
    {
        _currentEvent = _narrator.CurrentAct.CurrentDialogue;
        _gameManager.InputManager.ChangeInputLayer(InputLayer.Dialogue, _narrator.CurrentAct.CurrentDialogue.Options.Count);
        UpdateGUI();
        _gameManager.TextDescription.alignment = TextAnchor.UpperLeft;
        _gameManager.TextUI.alignment = TextAnchor.UpperLeft;
        _gameManager.GameProgress = GameProgress.DialogueBegin;
        _gameManager.AudioManager.PlayBGM(_gameManager.AudioManager.BGMNarrative);
        _narrator.Stop();
        _gameManager.InputManager.ResetSelection();
    }

    private void updateDialogueWithOptionGUI()
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
        else if (_narrator.Status == NarratorStatus.Completed || _narrator.Status == NarratorStatus.Skipped)
        {
            // If dialogue completed or skipped

            // if has options
            if (_narrator.CurrentAct.CurrentDialogue.HasOptions)
            {
                onHasOptions();
            }
            else
            {
                onNoOptions();
            }
        }
    }

    private void onHasOptions()
    {
        _gameManager.InputManager.ChangeInputLayer(InputLayer.ChooseDialogueOption, _narrator.CurrentAct.CurrentDialogue.Options.Count);
        _gameManager.TextDescription.text = _narrator.CurrentAct.CurrentDialogue.TextDescription;
        updateDialogueWithOptionGUI();
    }

    private void onNoOptions()
    {
        if (_currentEvent.DefaultEvent == null)
        {
            if (_narrator.CurrentAct.ActEnded)
            {
                // ToDo go to next Act
            }
            else
            {
                Debug.LogError("This is not the end of the act and it has no DefaultEvent or Options");
            }
        }
    }
    
    public void NextEvent()
    {
        switch (_currentEvent.GameEventType)
        {
            case GameEventType.Dialogue: // If next event is dialogue go to next dialogue
                if (((Dialogue)_currentEvent).HasOptions)
                {
                    GameEvent nextEvent = ((Dialogue)_currentEvent).Options[_gameManager.InputManager.SelectedItemIndex].NextEvent;
                    switch (nextEvent.GameEventType)
                    {
                        case GameEventType.Dialogue:
                            _currentEvent = _narrator.CurrentAct.NextDialogue((Dialogue)nextEvent);
                            _gameManager.InputManager.ChangeInputLayer(InputLayer.Dialogue, ((Dialogue)_currentEvent).Options.Count);
                            UpdateGUI();
                            break;
                        case GameEventType.Battle:
                            // If next event is battle go to battle
                            // CombatManager do stuff
                            break;
                    }
                    
                    Debug.Log("tO Next dialogue/ event");
                }
                else
                {
                    if (_currentEvent.DefaultEvent != null)
                    {
                        _currentEvent = _currentEvent.DefaultEvent;
                    }
                    else
                    {
                        Debug.Log("End of Dialogue");
                    }
                }
                break;
            case GameEventType.Battle:
                // If next event is battle go to battle
                // CombatManager do stuff
                break;
        }
    }
}