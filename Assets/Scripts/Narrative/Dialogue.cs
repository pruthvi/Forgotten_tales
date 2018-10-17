using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Forgotten Tale/Narrative/Dialogue", order = 2)]
public class Dialogue : GameEvent{

    public string Name = "Dialogue";
    [TextArea]
    public string TextDescription;
    public AudioClip AudioDescription;

    public List<Option> Options = new List<Option>();
    public void AddOption(Option o)
    {
        if (o != null)
        {
            o.Id = Id + "O" + (this.Options.Count + 1);
            this.Options.Add(o);
        }
    }
    public void RemoveOption(Option o)
    {
        if (o != null)
        {
            for(int i = 0; i < this.Options.Count; i++)
            {
                if (o.Id == this.Options[i].Id)
                {
                    this.Options.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
