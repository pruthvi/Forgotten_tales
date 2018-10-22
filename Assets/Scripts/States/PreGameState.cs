using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum PreGameProgress { Prologue, Intro }

public class PreGameState : GameState
{
    private int _currentIntroClipIndex;

    public PreGameProgress PreGameProgress;

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.PreGame;
        }
    }

    public override void OnGUIChange()
    {
        switch (PreGameProgress)
        {
            case PreGameProgress.Prologue:
                _gm.UIManager.Header = "Prologue";
                break;
            case PreGameProgress.Intro:
                _gm.UIManager.Header = "Intro";
                break;
        }
    }

    public override void OnEnter()
    {
        _gm.UIManager.TextHeader.alignment = TextAnchor.MiddleCenter;
        // Begin the first Act
        _gm.Narrator.BeginAct(0);

        // Play Prelogue
        PreGameProgress = PreGameProgress.Prologue;
        _gm.UIManager.Content = _gm.Narrator.CurrentAct.PrologueTextDescription;
        _gm.Narrator.Play(_gm.Narrator.CurrentAct.AudioPrologue);
        OnGUIChange();
    }

    public override void OnExit()
    {
        _gm.Narrator.SetToIdle();
    }

    public override void OnUpdate()
    {
        if (_gm.Narrator.CompletedOrSkipped)
        {
            if (_currentIntroClipIndex == _gm.Narrator.CurrentAct.AudioIntros.Length - 1)
            {
                _gm.ChangeState(GameStateType.InGame);
            }
        }
    }

    private void playIntro(int index)
    {
        OnGUIChange();
        _gm.UIManager.Content = _gm.Narrator.CurrentAct.IntroTextDescriptions[_currentIntroClipIndex];
        _gm.Narrator.Play(_gm.Narrator.CurrentAct.AudioIntros[_currentIntroClipIndex]);
    }

    public override void OnInput()
    {
        if (_gm.InputManager.SelectionSkipOrExit(true))
        {
            if (PreGameProgress == PreGameProgress.Prologue)
            {
                _gm.Narrator.Stop();
                PreGameProgress = PreGameProgress.Intro;
                playIntro(_currentIntroClipIndex);
                OnGUIChange();
            }
            else if(PreGameProgress == PreGameProgress.Intro)
            {
                if (_currentIntroClipIndex == _gm.Narrator.CurrentAct.AudioIntros.Length - 1)
                {
                    _gm.ChangeState(GameStateType.InGame);
                    return;
                }
                _currentIntroClipIndex++;
                playIntro(_currentIntroClipIndex);
            }
        }
    }
}
