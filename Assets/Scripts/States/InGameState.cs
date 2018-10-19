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

    public GameEvent CurrentGameEvent;

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
        State = State.Dialogue;
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
                        //NextEvent();

                        //_gameManager.InputManager.ChangeInputLayer(InputLayer.ChooseDialogueOption, ((Dialogue)_currentEvent).Options.Count);
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
    
    public void NextEvent()
    {
        // We already checked for Options
        // This only handles to next default event
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