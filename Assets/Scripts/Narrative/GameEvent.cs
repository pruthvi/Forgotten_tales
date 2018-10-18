using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum EventType { Dialogue, Combat }
public class GameEvent : ScriptableObject
{
    public string Id { get; set; }

    public GameEvent NextEvent;
    public EventType EventType { get; private set; }
}
