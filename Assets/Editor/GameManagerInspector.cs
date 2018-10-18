using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
[CanEditMultipleObjects]
public class GameManagerInspector : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager gm = target as GameManager;

        if (gm.InputManager != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Input Layer: ", GUILayout.MaxWidth(200));
            EditorGUILayout.EnumPopup(gm.InputManager.InputLayer);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Input Status: ", GUILayout.MaxWidth(200));
            EditorGUILayout.EnumPopup(gm.InputManager.InputStatus);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Input Selection Status: ", GUILayout.MaxWidth(200));
            EditorGUILayout.EnumPopup(gm.InputManager.SelectionStatus);
            GUILayout.EndHorizontal();
        }
        
    }
}