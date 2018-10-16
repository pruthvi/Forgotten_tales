using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Forgotten Tale/Narrative/Act")]
public class Act : ScriptableObject {

    public string Name = "Act";
    public AudioClip AudioName;
    [TextArea]
    public string PrologueTextDescription;
    public AudioClip AudioPrologue;

    public AudioClip[] AudioIntros;

    public Dialogue[] Dialogues;

}
