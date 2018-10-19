using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SettingsState : GameState
{

    // Settings
    public string[] _settingItems = { "BGM Volume: ", "SFX Volume: ", "Narrator Volume: ", "Narrator Speed: " };

    public SettingsState(GameManager gm) : base(gm)
    {

    }

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.Settings;
        }
    }

    public override void UpdateGUI()
    {
        _gameManager.TextUI.text = "Settings | [Esc] Back to Main Menu | [Up/Down] Select Option | [Left/Right] Adjust Setting";

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
                value = (int)((_gameManager.SettingManager.BGMVolume / _gameManager.SettingManager.MaxBGMVolumeValue) * 100) + " %";
            }
            else if (i == 1)
            {
                //    max = SettingManager.MaxSFXVolumeValue;
                //    min = SettingManager.MaxSFXVolumeValue;
                value = (int)((_gameManager.SettingManager.SFXVolume / _gameManager.SettingManager.MaxSFXVolumeValue) * 100) + " %";
            }
            else if (i == 2)
            {
                //    max = SettingManager.MaxNarratorVolume;
                //    min = SettingManager.MaxNarratorVolume;
                value = (int)((_gameManager.SettingManager.NarratorVolume / _gameManager.SettingManager.MaxNarratorVolume) * 100) + " %";
            }
            else if (i == 3)
            {
                //    max = SettingManager.MaxNarratorSpeedValue;
                //    min = SettingManager.MinNarratorSpeedValue;
                value = _gameManager.SettingManager.NarratorSpeed + "";
            }

            //settings += string.Format("{0} {1}\n\tMin: {2}\tMax: {3} Value: {4}\n", InputManager.SelectedItemIndex == i ? "> " : "\t", _settingItems[i], max, min, value);
            settings += (_gameManager.InputManager.SelectedItemIndex == i ? "> " : "\t") + _settingItems[i] + ": " + value + "\n";
        }

        _gameManager.TextDescription.text = settings;
    }

    public override void OnStateEnter()
    {
        _gameManager.Narrator.Stop();
        _gameManager.InputManager.ChangeInputLayer(InputLayer.Settings, _settingItems.Length);
        _gameManager.TextDescription.alignment = TextAnchor.UpperLeft;
        UpdateGUI();
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }
}