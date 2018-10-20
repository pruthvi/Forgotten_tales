using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dialogue))]
[CanEditMultipleObjects]
public class DialogueInspector : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        Dialogue d = target as Dialogue;

        EditorGUI.indentLevel++;
        for(int i = 0; i < d.Options.Count; i++)
        {
            d.Options[i].NextEvent = (GameEvent)EditorGUILayout.ObjectField("Next Event for Option " + (i + 1), d.Options[i].NextEvent, typeof(GameEvent), false);
        }
        EditorGUI.indentLevel--;
    }
}
