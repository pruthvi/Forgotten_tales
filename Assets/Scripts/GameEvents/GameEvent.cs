using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum GameEventType { Battle, Dialogue }
public abstract class GameEvent : ScriptableObject
{
    public string Id { get; private set; }
    public abstract GameEventType GameEventType { get; }
    public GameEvent DefaultEvent;
}
