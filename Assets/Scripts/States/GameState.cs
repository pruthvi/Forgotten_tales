using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum GameStateType { SplashScreen, MainMenu, PreGame, InGame, Battle, Controls, Settings }
public abstract class GameState
{
    protected GameManager _gameManager;
    public GameState(GameManager gm)
    {
        _gameManager = gm;
    }

    public abstract GameStateType GameStateType { get; }

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();

    public abstract void UpdateGUI();
}