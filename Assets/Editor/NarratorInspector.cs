using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Narrative;

[CustomEditor (typeof(Narrator))]
public class NarratorInspector : Editor {

    SerializedProperty actName;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Narrator n = (Narrator)target;

        EditorGUILayout.TextField("Act Name: ", n.CurrentAct.Name);

        int actionCounter = 0;
        foreach (Action action in n.CurrentAct.Actions.Values)
        {
            EditorGUILayout.TextField("Action " + ++actionCounter, action.Text);
            EditorGUILayout.Space();
            int optionCounter = 0;
            foreach (Option option in action.Options)
            {
                EditorGUILayout.TextField("Option " + ++optionCounter, option.Text, GUILayout.MaxWidth(100));
            }
        }

        if (GUILayout.Button("Add New Action", GUILayout.MaxWidth(40)))
        {

        }
    }

    void OnEnable()
    {
    }
}
