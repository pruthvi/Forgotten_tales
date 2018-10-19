using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PreGameState : GameState
{
    private Narrator _narrator;
    private int _currentIntroClipIndex;

    public PreGameState(GameManager gm) : base(gm)
    {
        _narrator = gm.Narrator;
    }

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.PreGame;
        }
    }

    public override void UpdateGUI()
    {
        switch (_gameManager.GameProgress)
        {
            case GameProgress.Prologue:
                _gameManager.TextUI.text = "Prologue";
                break;
            case GameProgress.Intro:
                _gameManager.TextUI.text = "Intro";
                break;
        }
    }

    public override void OnStateEnter()
    {
        _gameManager.GameProgress = GameProgress.Prologue;
        _gameManager.InputManager.ChangeInputLayer(InputLayer.Dialogue, 0);
        _gameManager.TextUI.alignment = TextAnchor.MiddleCenter;
        UpdateGUI();


        // Begin the first Act
        _narrator.BeginAct(0);
    }

    public override void OnStateExit()
    {
        _gameManager.GameProgress = GameProgress.DialogueBegin;
    }

    public override void OnStateUpdate()
    {
        switch (_gameManager.GameProgress)
        {
            case GameProgress.Prologue:
                if (_narrator.IsIdle)
                {
                    _gameManager.TextDescription.text = _narrator.CurrentAct.PrologueTextDescription;
                    _narrator.Play(_gameManager.Narrator.CurrentAct.AudioPrologue);
                    _gameManager.InputManager.ChangeInputLayer(InputLayer.Dialogue, 0);
                }
                if (_narrator.CompletedOrSkipped)
                {
                    _gameManager.GameProgress = GameProgress.Intro;
                    UpdateGUI();
                }
                break;
            case GameProgress.Intro:
                if (_narrator.IsIdle)
                {
                    _gameManager.TextDescription.text = _narrator.CurrentAct.IntroTextDescriptions[_currentIntroClipIndex];
                    _narrator.Play(_narrator.CurrentAct.AudioIntros[_currentIntroClipIndex]);
                }
                if (_narrator.CompletedOrSkipped)
                {
                    if (_currentIntroClipIndex + 1 < _narrator.CurrentAct.AudioIntros.Length)
                    {
                        _currentIntroClipIndex++;
                        _narrator.Stop();
                    }
                    else
                    {
                        _gameManager.ChangeState(GameStateType.InGame);
                    }
                }
                break;
        }
    }

    }
