using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum GameEventType { Battle, Dialogue }
public abstract class GameEvent : ScriptableObject, IGameEvent
{
    protected GameManager _gm;

    public string Id { get; private set; }
    public abstract GameEventType GameEventType { get; }

    public GameEvent DefaultEvent;

    public abstract void Init();
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();
    public abstract void OnGUIChange();
    public abstract void OnInput();
}
