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
    
    public List<Dialogue> Dialogues = new List<Dialogue>();

    //private int indexOf(Dialogue d)
    //{
    //    if(d == null)
    //    {
    //        return -1;
    //    }
    //    for (int i = 0; i < Dialogues.Count; i++)
    //    {
    //        if (Dialogues[i].name == d.name)
    //        {
    //            return i;
    //        }
    //    }
    //    return -1;
    //}
}
