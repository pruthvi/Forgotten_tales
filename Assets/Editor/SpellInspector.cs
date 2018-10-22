using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spell))]
[CanEditMultipleObjects]
public class SpellInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Spell s = target as Spell;
        s.name = EditorGUILayout.TextField("Name", s.name);

        s.BaseDamage = EditorGUILayout.FloatField("Base Damage", s.BaseDamage);
        s.DamageModifier = EditorGUILayout.FloatField("Damage Modifier", s.DamageModifier);
        s.Cost = EditorGUILayout.FloatField("Cost", s.Cost);

        s.SFXWhenFire = (AudioClip)EditorGUILayout.ObjectField("SFX when Fire", s.SFXWhenFire, typeof(AudioClip), false);
        s.SFXWhenHit = (AudioClip)EditorGUILayout.ObjectField("SFX when Hit", s.SFXWhenHit, typeof(AudioClip), false);

        EditorGUILayout.LabelField("Final Damage", s.FinalDamage.ToString());
    }
}
