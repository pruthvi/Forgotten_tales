using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Act))]
[CanEditMultipleObjects]
public class ActInspector : Editor {

    private bool showDialogues;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        Act act = target as Act;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        showDialogues = EditorGUILayout.Foldout(showDialogues, "Dialogues Set Up", true);
        if (showDialogues)
        {
            EditorGUI.indentLevel += 2;
            // Dialogue List
            for (int j = 0; j < act.Dialogues.Count; j++)
            {
                Dialogue d = act.Dialogues[j];
                d = (Dialogue)EditorGUILayout.ObjectField("Dialogue " + d.name, d, typeof(Dialogue), false);
                if (d.Options != null)
                {
                    if (d.Options.Count == 0)
                    {
                        d.DefaultEvent = (GameEvent)EditorGUILayout.ObjectField("Default Event", d.DefaultEvent, typeof(GameEvent), false);
                    }
                    else
                    {
                        GUILayout.Label("Option(s):");
                        for (int i = 0; i < d.Options.Count; i++)
                        {
                            d.Options[i] = (Option)EditorGUILayout.ObjectField("Option" + d.Options[i].name, d.Options[i], typeof(Option), false, GUILayout.Width(300));
                            d.Options[i].NextEvent = (GameEvent)EditorGUILayout.ObjectField("To", d.Options[i].NextEvent, typeof(GameEvent), false, GUILayout.Width(300));
                        }
                    }
                }
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            }
            EditorGUI.indentLevel -= 2;
        }
        
    }
}
