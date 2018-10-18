using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Act))]
[CanEditMultipleObjects]
public class ActInspector : Editor {

    private Dialogue newDialogue;
    private Option newOption;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        Act act = target as Act;

        /* This is replaced by DrawDefaultInspector
        // Name
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name: ");
        act.Name = EditorGUILayout.TextField("", act.Name);
        EditorGUILayout.EndHorizontal();

        // Prologue audio
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Prologue Clip: ");
        act.AudioPrologue = (AudioClip)EditorGUILayout.ObjectField(act.AudioPrologue, typeof(AudioClip), false);
        EditorGUILayout.EndHorizontal();

        // Prologue text description
        EditorGUILayout.LabelField("Prologue Text Description: ");
        act.PrologueTextDescription = EditorGUILayout.TextArea(act.PrologueTextDescription, GUILayout.Height(50));

        // Intro audios
        EditorGUILayout.LabelField("Intro Audios: ");

        // Intro List
        for (int i = 0; i < act.AudioIntros.Length; i++)
        {
            act.AudioIntros[i] = (AudioClip)EditorGUILayout.ObjectField(act.AudioIntros[i], typeof(AudioClip), false);
        }

        */

        // Dialogues
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Dialogues: ");
        //newDialogue = (Dialogue)EditorGUILayout.ObjectField(newDialogue, typeof(Dialogue), false);
        //if (GUILayout.Button("Add Dialogue"))
        //{
        //    //if (!act.AddDialogue(newDialogue))
        //    //{
        //    //    Debug.LogError("Dialogue already added or is null");
        //    //}
        //    act.AddDialogue(newDialogue);
        //    newDialogue = null;
        //}
        EditorGUILayout.EndHorizontal();



        // Dialogue List
        int index = 1;
        foreach (Dialogue d in act.Dialogues)
        {
            EditorGUILayout.LabelField("Dialogue " + index++ + ":");
            GUILayout.Space(5);
            EditorGUILayout.ObjectField(d, typeof(Dialogue), false);
            EditorGUI.indentLevel+=2;

            EditorGUILayout.LabelField("Option(s):");
            int optionIndex = 1;
            EditorGUI.indentLevel++;

            if (d.Options != null)
            {
                foreach (Option p in d.Options)
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Option " + optionIndex++ + ":");
                    EditorGUILayout.ObjectField(p, typeof(Option), false);
                    GUILayout.EndHorizontal();
                }
            }
           

            EditorGUI.indentLevel-=3;
            GUILayout.Space(10);
        }
    }
}
