using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dialogue))]
[CanEditMultipleObjects]
public class DialogueInspector : Editor {

    private Option option;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        Dialogue d = target as Dialogue;

        /*
        // Dialogues
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Options: ");
        option = (Option)EditorGUILayout.ObjectField(option, typeof(Option), false);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Option"))
        {
            d.AddOption(option);
            option = null;
        }
        if (GUILayout.Button("Remove Option"))
        {
            d.RemoveOption(option);
            option = null;
        }
        EditorGUILayout.EndHorizontal();
        */
        //int index = 1;
        //foreach (Option p in d.Options)
        //{
        //    GUILayout.BeginHorizontal();
        //    EditorGUILayout.LabelField("Option " + index++ + "("+ p.Id + "):" );
        //    EditorGUILayout.ObjectField(p, typeof(Option), false);
        //    GUILayout.EndHorizontal();
        //}
    }
}
