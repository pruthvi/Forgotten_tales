using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
[CanEditMultipleObjects]
public class PlayerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Player p = target as Player;

        p.name = EditorGUILayout.TextField("Name", p.name);

        p.MaxHP = EditorGUILayout.FloatField("Max HP", p.MaxHP);
        p.MaxMP = EditorGUILayout.FloatField("Max MP", p.MaxMP);

        p.HP = EditorGUILayout.FloatField("HP", p.HP);
        p.MP = EditorGUILayout.FloatField("MP", p.MP);

        DrawDefaultInspector();
    }
}