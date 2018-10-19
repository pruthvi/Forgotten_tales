﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Forgotten Tale/Narrative/Act", order = 1)]
public class Act : ScriptableObject {

    public string Name = "Act";
    public AudioClip AudioName;
    [TextArea]
    public string PrologueTextDescription;
    public AudioClip AudioPrologue;

    public AudioClip[] AudioIntros;
    [TextArea]
    public string[] IntroTextDescriptions;

    private int _currentDialogueIndex;

    public Dialogue CurrentDialogue
    {
        get
        {
            return Dialogues[_currentDialogueIndex];
        }
    }

    public bool ActEnded
    {
        get
        {
            return _currentDialogueIndex == Dialogues.Count - 1;
        }
    }

    public List<Dialogue> Dialogues = new List<Dialogue>();
    
    public Dialogue NextDialogue(Dialogue d)
    {
        int index = indexOf(d);
        if (index >= 0 && index < Dialogues.Count)
        {
            _currentDialogueIndex = index;
            Debug.Log(index);
            return CurrentDialogue;
        }
        return null;
    }

    //public bool NextEventBasedOnOption(int index)
    //{
    //    if (_currentGameEvent.GameEventType == GameEventType.Dialogue)
    //    {
    //        // If has option then go to the next event based on selected index
    //        if (CurrentDialogue.HasOptions)
    //        {
    //            // Check if valid selection
    //            if (index < CurrentDialogue.Options.Count)
    //            {
    //                _currentGameEvent = CurrentDialogue.Options[index].NextEvent;
    //                if (_currentGameEvent.GameEventType == GameEventType.Dialogue)
    //                {
    //                    _currentDialogueIndex++;
    //                    return true;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            // If no option go to the default event

    //            // Check for default next event type
    //            if (CurrentDialogue.DefaultEvent.GameEventType == GameEventType.Dialogue)
    //            {
    //                _currentDialogueIndex++;
    //                _currentGameEvent = Dialogues[_currentDialogueIndex];
    //            }
    //            else
    //            {
    //                _currentGameEvent = _currentGameEvent.DefaultEvent;
    //                GameManager.Instance.ChangeState(GameState.Battle);
    //            }
    //        }
    //        return true;
    //    }
    //    // If no options and no default event log error
    //    if (!CurrentDialogue.IsEndOfAct)
    //    {
    //        Debug.LogError("Current dialogue does not have options, and does not have NextEvent to go to.");
    //    }
    //    return false;
    //}

    private int indexOf(Dialogue d)
    {
        if(d == null)
        {
            return -1;
        }
        for (int i = 0; i < Dialogues.Count; i++)
        {
            if (Dialogues[i].name == d.name)
            {
                return i;
            }
        }
        return -1;
    }

    //public void AddDialogue(Dialogue d)
    //{
    //    if (d != null)
    //    {
    //        Dialogues.Add(d);
    //    }
    //}

    public void Init()
    {
        _currentDialogueIndex = 0;
    }
}
