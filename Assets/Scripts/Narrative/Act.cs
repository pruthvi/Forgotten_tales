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
    //public Dictionary<string, Dialogue> Dialogues { get; private set; }

    //public List<string> Keys;
    public List<Dialogue> Dialogues;

    public Act()
    {
        Dialogues = new List<Dialogue>();
        //Dialogues = new Dictionary<string, Dialogue>();
    }

    public void NextDialogue(int index)
    {
        // check if is valid selection
        if (index < CurrentDialogue.Options.Count)
        {
            _currentDialogueIndex = indexOf((Dialogue)CurrentDialogue.Options[index].NextEvent);
        }


        if(_currentDialogueIndex + 1 < Dialogues.Count)
            _currentDialogueIndex++;
    }

    private int indexOf(Dialogue d)
    {
        for (int i = 0; i < Dialogues.Count; i++)
        {
            if (Dialogues[i].Id == d.Id)
            {
                return i;
            }
        }
        return -1;
    }

    public void AddDialogue(Dialogue d)
    {
        if (d != null)
        {
            Dialogues.Add(d);
        }
        //Dialogue tempD = null;
        //if (!Dialogues.TryGetValue(d.Id, out tempD))
        //{
        //    Dialogues.Add(d.Id, d);
        //    return true;
        //}
    }
}
