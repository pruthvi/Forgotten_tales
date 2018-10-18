﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputLayer { SplashScreen, MainMenu, Controls, Settings, Dialogue, ChooseDialogueOption, ChoosePreCombatOption, ChooseCombatOption }
public enum InputStatus { Idle, Selecting, Completed }
public enum SelectionStatus { None, Valid, Invalid }

public class InputManager {

    public InputLayer InputLayer { get; private set; }
    
    public InputStatus InputStatus { get; private set; }

    public SelectionStatus SelectionStatus { get; private set; }

    public int MaxItemCount { get; private set; }

    public int SelectedItemIndex { get; private set; }

    public float MaxAdjustmentValue;
    public float MinAdjustmentValue;
    public float AdjustmentValue { get; private set; }
    public float AdjustmentRatio { get; private set; }

    private GameManager _gameManager;
    private SettingManager _settingManager;

    public InputManager(GameManager gameManager)
    {
        _gameManager = gameManager;
        _settingManager = gameManager.SettingManager;
        InputLayer = InputLayer.SplashScreen;
        SelectedItemIndex = 0;
    }

    public void Update() {
        switch (InputLayer)
        {
            case InputLayer.SplashScreen:
                updateSplashScreenLayer();
                break;
            case InputLayer.MainMenu:
                updateMainMenuLayer();
                break;
            case InputLayer.Controls:
                updateControlsLayer();
                break;
            case InputLayer.Settings:
                updateSettingsLayer();
                break;
            case InputLayer.Dialogue:
                updateDialogueLayer();
                break;
            case InputLayer.ChooseDialogueOption:
                updateChooseDialogueOptionLayer();
                break;
            case InputLayer.ChoosePreCombatOption:
                updateChoosePreCombatOptionLayer();
                break;
            case InputLayer.ChooseCombatOption:
                updateChooseCombatOptionLayer();
                break;
        }
    }

    // Handle Inputs for SplashScreen if any
    private void updateSplashScreenLayer()
    {
        // Skip splash screen
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    _gameManager.ChangeState(GameState.MainMenu);
        //}
    }

    // Handle Inputs for MainMenu if any
    private void updateMainMenuLayer()
    {
        // Only Take Input for Up/Down Arrow Key and Return Key
        if (selectionUp())
        {
            _gameManager.UpdateMainMenuGUI();
        }
        if (selectionDown())
        {
            _gameManager.UpdateMainMenuGUI();
        }
        if (selectionConfirm())
        {
            if (SelectedItemIndex == 0)
            {
                // Start
                _gameManager.StartGame();
            }
            else if (SelectedItemIndex == 1)
            {
                // Controls
                _gameManager.ChangeState(GameState.Controls);
            }
            else if (SelectedItemIndex == 2)
            {
                // Settings
                _gameManager.ChangeState(GameState.Settings);
            }
            else if (SelectedItemIndex == 3)
            {
                // Exit
                Application.Quit();
            }
        }
    }

    // Handle Inputs for Controls if any
    private void updateControlsLayer()
    {
        if (goBack())
        {
            _gameManager.ChangeState(GameState.MainMenu);
        }
    }

    // Handle Inputs for Settings if any
    private void updateSettingsLayer()
    {
        // Only Take Input for Arrow Keys and Return Key
        if (goBack())
        {
            _gameManager.ChangeState(GameState.MainMenu);
        }

        // Up/Down for Setting Options
        if (selectionUp() || selectionDown())
        {
            if (SelectedItemIndex == 0)
            {
                // BGM Volume
                SetAdjustmentLimit(_settingManager.MaxBGMVolumeValue, _settingManager.MinBGMVolumeValue);
                SetAdjustmentRatio(_gameManager.SettingManager.BGMVolumeAdjustmentRatio);
                AdjustmentValue = _gameManager.SettingManager.BGMVolume;
            }
            else if (SelectedItemIndex == 1)
            {
                // SFX Volume
                SetAdjustmentLimit(_settingManager.MaxSFXVolumeValue, _settingManager.MinSFXVolumeValue);
                SetAdjustmentRatio(_gameManager.SettingManager.SFXVolumeAdjustmentRatio);
                AdjustmentValue = _gameManager.SettingManager.SFXVolume;
            }
            else if (SelectedItemIndex == 2)
            {
                // Narrator Volume
                SetAdjustmentLimit(_settingManager.MaxNarratorVolume, _settingManager.MinNarratorVolume);
                SetAdjustmentRatio(_gameManager.SettingManager.NarratorVolumeAdjustmentRatio);
                AdjustmentValue = _gameManager.SettingManager.NarratorVolume;
            }
            _gameManager.UpdateSettingsGUI();
        }

        // Left/Right for value
        if (adjustIncrease() || adjustDecrease())
        {
            if (SelectedItemIndex == 0)
            {
                // BGM Volume
                _gameManager.SettingManager.BGMVolume = AdjustmentValue;
            }
            else if (SelectedItemIndex == 1)
            {
                // SFX Volume
                _gameManager.SettingManager.SFXVolume = AdjustmentValue;
            }
            else if (SelectedItemIndex == 2)
            {
                // Narrator Volume
                _gameManager.SettingManager.NarratorVolume = AdjustmentValue;
            }
            else if (SelectedItemIndex == 3)
            {
                // Narrator Speed
                _gameManager.Narrator.FastForward();
               // _gameManager.SettingManager.NarratorSpeed = AdjustmentValue;
            }
            _gameManager.UpdateSettingsGUI();
        }
    }

    // Handle Inputs for Dialogue if any
    private void updateDialogueLayer()
    {
        // Skip
        if (goBack())
        {
            _gameManager.Narrator.Skip();
            _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXSkip);
        }
        // Fast forward
        if (Input.GetKeyDown(KeyCode.F))
        {
            _gameManager.Narrator.FastForward();
            _gameManager.UpdateInGameGUI();
            _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXMenuItemSelection);

        }
        // Replay
        if (Input.GetKeyDown(KeyCode.R))
        {
            _gameManager.Narrator.Replay(PlayType.Dialogue);
        }
    }

    // Handle Inputs for ChooseDialogueOption if any
    private void updateChooseDialogueOptionLayer()
    {
        // Choose Dialogue Option
        if (selectionUp())
        {
            _gameManager.UpdateDialogueWithOptionGUI();
        }
        if (selectionDown())
        {
            _gameManager.UpdateDialogueWithOptionGUI();
        }
        // Confirm Dialogue Option
        if (selectionConfirm())
        {
            if (_gameManager.CurrentAct.NextDialogue(SelectedItemIndex))
            {
                InputStatus = InputStatus.Completed;
                SelectionStatus = SelectionStatus.Valid;
                _gameManager.Narrator.SetToIdle();
                _gameManager.GameProgress = GameProgress.Dialogue;
            }
        }
        // Fast forward
        if (Input.GetKeyDown(KeyCode.F))
        {
            _gameManager.Narrator.FastForward();
            _gameManager.UpdateInGameGUI();
        }

        // Replay Option
        if (Input.GetKeyDown(KeyCode.R))
        {
            _gameManager.Narrator.Replay(PlayType.Option);
        }

        // Replay Dialogue
        if (Input.GetKeyDown(KeyCode.T))
        {
            _gameManager.Narrator.Replay(PlayType.Dialogue);
        }
    }

    // Handle Inputs for ChoosePreCombatOption if any
    private void updateChoosePreCombatOptionLayer()
    {

    }

    // Handle Inputs for ChooseCombatOption if any
    private void updateChooseCombatOptionLayer()
    {

    }

    public void ChangeInputLayer(InputLayer layer, int maxItemCount)
    {
        this.InputLayer = layer;
        setMaxItemCount(maxItemCount);
    }

    public void SetAdjustmentLimit(float max, float min)
    {
        if (max > min)
        {
            MaxAdjustmentValue = max;
            MinAdjustmentValue = min;
        }
        else
        {
            MaxAdjustmentValue = min;
            MinAdjustmentValue = max;
        }
    }

    public void SetAdjustmentRatio(float ratio)
    {
        if (ratio > 0 && ratio < MaxAdjustmentValue)
        {
            AdjustmentRatio = ratio;
        }
    }

    public void ResetSelection()
    {
        this.InputStatus = InputStatus.Idle;
        this.SelectionStatus = SelectionStatus.None;
    }

    private void setMaxItemCount(int count)
    {
        // Only set MaxItemCount to count if the item count is greater than 1, and set the default SelectedItemIndex to 0
        if (count > 1)
        {
            MaxItemCount = count;
            SelectedItemIndex = 0;
        }
        else
        {
            SelectedItemIndex = -1;
        }    
    }

    private bool selectionUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (SelectedItemIndex - 1 < 0)
            {
                SelectedItemIndex = MaxItemCount - 1;
            }
            else
            {
                SelectedItemIndex--;
            }
            _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXMenuItemSelection);
            return true;
        }
        return false;
    }

    private bool selectionDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (SelectedItemIndex + 1 >= MaxItemCount)
            {
                SelectedItemIndex = 0;
            }
            else
            {
                SelectedItemIndex++;
            }
            _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXMenuItemSelection);
            return true;
        }
        return false;
    }

    private bool adjustIncrease()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (AdjustmentValue + AdjustmentRatio < MaxAdjustmentValue)
            {
                AdjustmentValue += AdjustmentRatio;
            }
            else
            {
                AdjustmentValue = MaxAdjustmentValue;
            }
            _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXMenuItemSelection);
            return true;
        }
        return false;
    }

    private bool adjustDecrease()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (AdjustmentValue - AdjustmentRatio > MinAdjustmentValue)
            {
                AdjustmentValue -= AdjustmentRatio;
            }
            else
            {
                AdjustmentValue = MinAdjustmentValue;
            }
            _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXMenuItemSelection);
            return true;
        }
        return false;
    }

    private bool selectionConfirm()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXConfirm);
            return true;
        }
        return false;
    }

    private bool goBack()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ToDo: Play back SFX
            return true;
        }
        return false;
    }
    public bool InputCompleted
    {
        get
        {
            return InputStatus == InputStatus.Completed;
        }
    }

    public bool InputCompletedOrIsValid
    {
        get
        {
            return (InputStatus == InputStatus.Completed || SelectionStatus == SelectionStatus.Valid);
        }
    }

    public bool ValidInput
    {
        get
        {
            return SelectionStatus == SelectionStatus.Valid;
        }
    }
}