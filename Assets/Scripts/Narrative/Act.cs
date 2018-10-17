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
            if (indexOfDialogue == -1)
            {
                return false;
            }
            else
            {
                Debug.Log(_currentDialogueIndex);
                if (_currentDialogueIndex + 1 < Dialogues.Count)
                {
                    _currentDialogueIndex++;
                    Debug.Log(_currentDialogueIndex + "++");
                    return true;
                }
            }
            
        }
        return false;
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
            d.Id = "A" + Dialogues.Count;
            Dialogues.Add(d);
        }
    }
}
