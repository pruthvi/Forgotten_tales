using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum GameStateType { SplashScreen, MainMenu, Controls, Setting, PreGame, InGame, Battle }
// This does have to be MonoBehaviour, and will be fixed when writing custom inspector
public abstract class GameState : MonoBehaviour, IGameEvent
{
    protected GameManager _gm;

    public virtual void InitState()
    {
        _gm = GameManager.Instance;
    }

    // Called when State enter
    public abstract void OnEnter();

    // Called when State exit
    public abstract void OnExit();

    // Handle Update
    public abstract void OnUpdate();

    // Handle GUI
    public abstract void OnGUIChange();

    // Handle User Input
    public abstract void OnInput();

    public abstract GameStateType GameStateType { get; }
}