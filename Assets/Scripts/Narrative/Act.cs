using System.Collections;
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

    public bool NextDialogue(int index)
    {
        // check if is valid selection
        if (index < CurrentDialogue.Options.Count)
        {
            int indexOfDialogue = indexOf((Dialogue)CurrentDialogue.Options[index].NextEvent);
            if(indexOfDialogue != -1)
            {
                _currentDialogueIndex = indexOfDialogue;
                return true;
            }
        }
        return false;
    }

    public bool NextDialogue()
    {
        // check if is valid selection
        if (CurrentDialogue.Options.Count == 0)
        {
            int indexOfDialogue = indexOf((Dialogue)CurrentDialogue.NextEvent);
            if (indexOfDialogue != -1)
            {
                _currentDialogueIndex = indexOfDialogue;
                return true;
            }
        }
        if (!CurrentDialogue.IsEndOfAct)
        {
            Debug.LogError("Current dialogue does not have options, and does not have NextEvent to go to.");
        }
        return false;
    }

    private int indexOf(Dialogue d)
    {
        if(d == null)
        {
            return -1;
        }
        for (int i = 0; i < Dialogues.Count; i++)
        {
            if (Dialogues[i].Id == d.Id)
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
