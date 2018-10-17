using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dialogue))]
[CanEditMultipleObjects]
public class DialogueInspector : Editor {

    private Option newOption;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        Dialogue d = target as Dialogue;

        // Dialogues
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Options: ");
        newOption = (Option)EditorGUILayout.ObjectField(newOption, typeof(Option), false);
        if (GUILayout.Button("Add Option"))
        {
            d.AddOption(newOption);
            newOption = null;
        }
        EditorGUILayout.EndHorizontal();

        int index = 1;
        foreach (Option p in d.Options)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Option " + index++ + ":");
            EditorGUILayout.ObjectField(p, typeof(Option), false);
            GUILayout.EndHorizontal();
        }
    }
}
