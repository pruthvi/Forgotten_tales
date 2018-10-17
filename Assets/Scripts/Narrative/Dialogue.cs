using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Forgotten Tale/Narrative/Dialogue", order = 2)]
public class Dialogue : GameEvent{

    private static int nextActionId = 0;

    public string Id { get; private set; }
    public List<Option> Options { get; private set; }

    public string Name = "Dialogue";
    [TextArea]
    public string TextDescription;
    public AudioClip AudioDescription;

    //public EventType EventType = EventType.Dialogue;

    public Dialogue()
    {
        Id = "D" + (nextActionId++);
        Options = new List<Option>();
    }

    public void AddOption(Option o)
    {
        if (o != null)
        {
            this.Options.Add(o);
        }
    }
}
