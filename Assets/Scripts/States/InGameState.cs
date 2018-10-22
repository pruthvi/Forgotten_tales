using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
    }

    public Player Player;

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.InGame;
        }
    }

    public override void OnGUIChange()
    {
        string text = textInstruction + "Speed: x" + _gm.Narrator.Speed;
        _gm.UIManager.Header = text;
    }

    public override void OnEnter()
    {
        _gm.Narrator.Stop();
        _gm.UIManager.TextContent.alignment = TextAnchor.UpperLeft;
        _gm.UIManager.TextHeader.alignment = TextAnchor.UpperLeft;
        _gm.AudioManager.Play(_gm.AudioManager.BGMNarrative, AudioChannel.BGM);
        _currentEvent = _gm.Narrator.FirstDialogue;
        _currentEvent.OnEnter();
        OnGUIChange();
    }

    public void NextEvent(GameEvent nextNext = null)
    {
        if (_currentEvent == null)
        {
            return;
        }
        _currentEvent.OnExit();
        if (nextNext == null)
        {
            if (_currentEvent.DefaultEvent != null)
            {

                _currentEvent = _currentEvent.DefaultEvent;
            }
            else
            {
                Debug.LogError("No default event or options");
            }
        }
        else
        {
            _currentEvent = nextNext;
        }
        _currentEvent.OnEnter();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        if (_currentEvent != null)
        {
            _currentEvent.OnInput();
            _currentEvent.OnUpdate();
        }
    }

    public override void OnInput()
    {
    }
}