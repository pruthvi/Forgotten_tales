using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using Assets.Scripts.Setup;
using Assets.Scripts.Narrative;
using UnityEngine;
using UnityEngine.Audio;

[CustomEditor(typeof(ActInitializer))]
public class ActInitializerInspector : Editor
{
    SerializedProperty actionIdProp;
    SerializedProperty audioClipProp;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ActInitializer ai = (ActInitializer)target;

        Dictionary<string, Action> actions = ai.Actions;

        EditorGUILayout.TextField("Action Name");
        EditorGUILayout.ObjectField(audioClipProp);

        if (GUILayout.Button("Add New Action", GUILayout.MaxWidth(40)))
        {
         //   actions.Add();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
