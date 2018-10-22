﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioManager))]
[CanEditMultipleObjects]
public class AudioManagerInspector : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AudioManager am = target as AudioManager;

        if (am.BackgroundSource != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("BGM Speed: ", GUILayout.MaxWidth(200));
            am.BackgroundSource.pitch = GUILayout.HorizontalSlider(am.BackgroundSource.pitch, -3, 3);
            am.BackgroundSource.pitch = EditorGUILayout.FloatField(am.BackgroundSource.pitch, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("BGM Volume: ", GUILayout.MaxWidth(200));
            am.BackgroundSource.volume = GUILayout.HorizontalSlider(am.BackgroundSource.volume, 0, 1);
            am.BackgroundSource.volume = EditorGUILayout.FloatField(am.BackgroundSource.volume, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
        }

        if (am.SFXSource != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("SFX Speed: ", GUILayout.MaxWidth(200));
            am.SFXSource.pitch = GUILayout.HorizontalSlider(am.SFXSource.pitch, -3, 3);
            am.SFXSource.pitch = EditorGUILayout.FloatField(am.SFXSource.pitch, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("SFX Volume: ", GUILayout.MaxWidth(200));
            am.SFXSource.volume = GUILayout.HorizontalSlider(am.SFXSource.volume, 0, 1);
            am.SFXSource.volume = EditorGUILayout.FloatField(am.SFXSource.volume, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
        }

        if (am.NarrativeSource != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Narrator Speed: ", GUILayout.MaxWidth(200));
            am.NarrativeSource.pitch = GUILayout.HorizontalSlider(am.NarrativeSource.pitch, -3, 3);
            am.NarrativeSource.pitch = EditorGUILayout.FloatField(am.NarrativeSource.pitch, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Narrator Volume: ", GUILayout.MaxWidth(200));
            am.NarrativeSource.volume = GUILayout.HorizontalSlider(am.NarrativeSource.volume, 0, 1);
            am.NarrativeSource.volume = EditorGUILayout.FloatField(am.NarrativeSource.volume, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
        }
    }
}