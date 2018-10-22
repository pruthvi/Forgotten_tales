using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioManager))]
[CanEditMultipleObjects]
public class AudioManagerInspector : Editor
{

    public override void OnInspectorGUI()
    {
        AudioManager am = target as AudioManager;

        if (am.BGMChannel != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("BGM Speed: ", GUILayout.MaxWidth(200));
            am.BGMChannel.pitch = GUILayout.HorizontalSlider(am.BGMChannel.pitch, -3, 3);
            am.BGMChannel.pitch = EditorGUILayout.FloatField(am.BGMChannel.pitch, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("BGM Volume: ", GUILayout.MaxWidth(200));
            am.BGMChannel.volume = GUILayout.HorizontalSlider(am.BGMChannel.volume, 0, 1);
            am.BGMChannel.volume = EditorGUILayout.FloatField(am.BGMChannel.volume, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
        }

        if (am.SFX1Channel != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("SFX 1 Speed: ", GUILayout.MaxWidth(200));
            am.SFX1Channel.pitch = GUILayout.HorizontalSlider(am.SFX1Channel.pitch, -3, 3);
            am.SFX1Channel.pitch = EditorGUILayout.FloatField(am.SFX1Channel.pitch, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("SFX 1 Volume: ", GUILayout.MaxWidth(200));
            am.SFX1Channel.volume = GUILayout.HorizontalSlider(am.SFX1Channel.volume, 0, 1);
            am.SFX1Channel.volume = EditorGUILayout.FloatField(am.SFX1Channel.volume, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
        }

        if (am.SFX2Channel != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("SFX 2 Speed: ", GUILayout.MaxWidth(200));
            am.SFX2Channel.pitch = GUILayout.HorizontalSlider(am.SFX2Channel.pitch, -3, 3);
            am.SFX2Channel.pitch = EditorGUILayout.FloatField(am.SFX2Channel.pitch, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("SFX 2 Volume: ", GUILayout.MaxWidth(200));
            am.SFX2Channel.volume = GUILayout.HorizontalSlider(am.SFX2Channel.volume, 0, 1);
            am.SFX2Channel.volume = EditorGUILayout.FloatField(am.SFX2Channel.volume, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
        }

        if (am.NarratorChannel != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Narrator Speed: ", GUILayout.MaxWidth(200));
            am.NarratorChannel.pitch = GUILayout.HorizontalSlider(am.NarratorChannel.pitch, -3, 3);
            am.NarratorChannel.pitch = EditorGUILayout.FloatField(am.NarratorChannel.pitch, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Narrator Volume: ", GUILayout.MaxWidth(200));
            am.NarratorChannel.volume = GUILayout.HorizontalSlider(am.NarratorChannel.volume, 0, 1);
            am.NarratorChannel.volume = EditorGUILayout.FloatField(am.NarratorChannel.volume, GUILayout.MaxWidth(50));
            GUILayout.EndHorizontal();
        }
    }
}
