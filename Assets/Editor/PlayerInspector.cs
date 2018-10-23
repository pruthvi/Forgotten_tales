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

        p.Name = EditorGUILayout.TextField("Name", p.Name);

        p.MaxHP = EditorGUILayout.FloatField("Max HP", p.MaxHP);
        p.MaxMP = EditorGUILayout.FloatField("Max MP", p.MaxMP);

        p.HP = EditorGUILayout.FloatField("HP", p.HP);
        p.MP = EditorGUILayout.FloatField("MP", p.MP);

        //for(int i = 0; i < p.InventoryManager.MaxItemSlot; i++)
        //{
        //    p.InventoryManager.Items[i] = (Item)EditorGUILayout.ObjectField("Item " + i, p.InventoryManager.Items[i], typeof(Item), false);
        //}

        DrawDefaultInspector();
    }
}