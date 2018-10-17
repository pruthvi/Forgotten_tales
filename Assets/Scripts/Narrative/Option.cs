using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Forgotten Tale/Narrative/Option", order = 3)]
public class Option : ScriptableObject {

    public string Name = "Option";
    [TextArea]
    public string TextDescription;
    public AudioClip AudioDescription;
    public GameEvent NextEvent;
 
}
