using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
[CanEditMultipleObjects]
public class EnemyInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Enemy e = target as Enemy;

        e.name = EditorGUILayout.TextField("Name", e.name);

        e.MaxHP = EditorGUILayout.FloatField("Max HP", e.MaxHP);
        e.MaxMP = EditorGUILayout.FloatField("Max MP", e.MaxMP);

        e.HP = EditorGUILayout.FloatField("HP", e.HP);
        e.MP = EditorGUILayout.FloatField("MP", e.MP);

        DrawDefaultInspector();
    }
}
