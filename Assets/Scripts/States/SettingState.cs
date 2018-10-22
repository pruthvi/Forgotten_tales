using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SettingState : GameState
{

    // Settings
    public string[] _settingItems = { "BGM Volume: ", "SFX Volume: ", "Narrator Volume: ", "Narrator Speed: " };
    
    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.Setting;
        }
    }

    public override void OnGUIChange()
    {
        _gm.UIManager.Header = "Settings | [Esc] Back to Main Menu | [Up/Down] Select Option | [Left/Right] Adjust Setting";

        string settings = "";

        for (int i = 0; i < _settingItems.Length; i++)
        {
            //float max = 0;
            //float min = 0;
            string value = "";

            if (i == 0)
            {
                //    max = SettingManager.MaxBGMVolumeValue;
                //    min = SettingManager.MinBGMVolumeValue;
                value = (int)((_gm.SettingManager.BGMVolume / _gm.SettingManager.MaxBGMVolumeValue) * 100) + " %";
            }
            else if (i == 1)
            {
                //    max = SettingManager.MaxSFXVolumeValue;
                //    min = SettingManager.MaxSFXVolumeValue;
                value = (int)((_gm.SettingManager.SFXVolume / _gm.SettingManager.MaxSFXVolumeValue) * 100) + " %";
            }
            else if (i == 2)
            {
                //    max = SettingManager.MaxNarratorVolume;
                //    min = SettingManager.MaxNarratorVolume;
                value = (int)((_gm.SettingManager.NarratorVolume / _gm.SettingManager.MaxNarratorVolume) * 100) + " %";
            }
            else if (i == 3)
            {
                //    max = SettingManager.MaxNarratorSpeedValue;
                //    min = SettingManager.MinNarratorSpeedValue;
                value = _gm.SettingManager.NarratorSpeed + "";
            }

            //settings += string.Format("{0} {1}\n\tMin: {2}\tMax: {3} Value: {4}\n", InputManager.SelectedItemIndex == i ? "> " : "\t", _settingItems[i], max, min, value);
            settings += (_gm.InputManager.SelectedItemIndex == i ? "> " : "\t") + _settingItems[i] + ": " + value + "\n";
        }

        _gm.UIManager.Content = settings;
    }

    public override void OnEnter()
    {
        _gm.Narrator.Stop();
        _gm.UIManager.TextHeader.alignment = TextAnchor.UpperLeft;
        OnGUIChange();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }

    public override void OnInput()
    {
        if (_gm.InputManager.SelectionSkipOrExit(false))
        {
            _gm.ChangeState(GameStateType.MainMenu);
        }
    }
}