using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum State { Dialogue, DialogueOption }

public class InGameState : GameState
{

    private string textInstruction = "[R] - Replay | [T] - Replay Dialogue\n[F] Fast Forward | [Esc] Skip | ";

    public State State;

    private Narrator _narrator;

    private GameEvent _currentEvent;

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
        _gameManager.InputManager.ChangeInputLayer(InputLayer.Dialogue, 0);
        UpdateGUI();
        _gameManager.TextDescription.alignment = TextAnchor.UpperLeft;
        _gameManager.TextUI.alignment = TextAnchor.UpperLeft;
        _gameManager.GameProgress = GameProgress.DialogueBegin;
        _gameManager.AudioManager.PlayBGM(_gameManager.AudioManager.BGMNarrative);
        _narrator.Stop();
        State = State.Dialogue;
        _currentEvent = _narrator.CurrentAct.CurrentDialogue;
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
        switch (State)
        {
            case State.Dialogue:
                if (_narrator.IsIdle)
                {
                    _gameManager.TextDescription.text = _narrator.CurrentAct.CurrentDialogue.TextDescription;
                    _narrator.Play(_narrator.CurrentAct.CurrentDialogue.AudioDescription, PlayType.Dialogue);
                    _gameManager.InputManager.ChangeInputLayer(InputLayer.Dialogue, 0);
                }
                if (_gameManager.Narrator.CompletedOrSkipped)
                {
                    //if (_narrator.CurrentAct.CurrentDialogue.IsEndOfAct)
                    //{
                    //    GameState = GameState.EndAct;
                    //    return;
                    //}

                    if (((Dialogue)_currentEvent).HasOptions)
                    {
                        // Prompt player for input
                        _gameManager.InputManager.ChangeInputLayer(InputLayer.ChooseDialogueOption, ((Dialogue)_currentEvent).Options.Count);
                        UpdateGUI();
                        State = State.DialogueOption;
                        updateDialogueWithOptionGUI();
                    }
                    else
                    {
                        // Go to default event
                    }

                    //// If there is option prompt for input
                    //if ()
                    //{
                    //    GameProgress = GameProgress.DialogueOption;
                    //    InputManager.ChangeInputLayer(InputLayer.ChooseDialogueOption, _currentAct.CurrentDialogue.Options.Count);
                    //    Narrator.Play(_currentAct.CurrentDialogue.Options[InputManager.SelectedItemIndex].AudioDescription, PlayType.Option);
                    //    UpdateDialogueWithOptionGUI();
                    //}
                    //else
                    //{
                    //    if (_currentAct.CurrentGameEvent.GameEventType == GameEventType.Battle)
                    //    {
                    //        Debug.Log("Prepare for Battle");
                    //        GameState = GameState.Battle;
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        //_currentAct.NextEvent();
                    //        GameProgress = GameProgress.Dialogue;
                    //        Narrator.SetToIdle();
                    //    }
                    //}
                }
                break;
            case State.DialogueOption:
                //if (InputManager.InputCompleted)
                //{
                //    NextEvent();
                //}
                break;
        }
    }
    }