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
    
    public Dialogue NextDialogue(Dialogue d)
    {
        Debug.Log("Called");
        int index = indexOf(d);
        if (index >= 0 && index < Dialogues.Count)
        {
            _currentDialogueIndex = index;
            Debug.Log(index);
            return CurrentDialogue;
        }
        return null;
    }

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

    public void Init()
    {
        Debug.Log("Act Init");
        _currentDialogueIndex = 0;
    }
}
