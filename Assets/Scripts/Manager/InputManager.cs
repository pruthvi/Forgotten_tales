using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum InputLayer { SplashScreen, MainMenu, Controls, Settings, Dialogue, ChooseDialogueOption, ChoosePreCombatOption, ChooseCombatOption, ChooseItemOption }
public enum SelectionStatus { None, Valid, Invalid }

public class InputManager : MonoBehaviour
{
    public InputLayer InputLayer;
    public SelectionStatus SelectionStatus;

    public int MenuItemSelectionLimit { get; private set; }
    public int SelectedItemIndex { get; private set; }
    private static GameManager _gm;

    void Start()
    {
        _gm = GameManager.Instance;
    }

    //public float MaxAdjustmentValue;
    //public float MinAdjustmentValue;
    //public float AdjustmentValue;
    //public float AdjustmentRatio;

    public AudioClip[] SFX;

    //    private GameManager _gameManager;
    //    private SettingManager _settingManager;

    //    private AudioClip[] _clipMenuItems;

    //    public InputManager(GameManager gameManager)
    //    {
    //        _gameManager = gameManager;
    //        _settingManager = gameManager.SettingManager;
    //        InputLayer = InputLayer.SplashScreen;
    //        SelectedItemIndex = 0;
    //    }

    //    public void Update()
    //    {
    //        switch (InputLayer)
    //        {
    //            case InputLayer.SplashScreen:
    //                updateSplashScreenLayer();
    //                break;
    //            case InputLayer.MainMenu:
    //                updateMainMenuLayer();
    //                break;
    //            case InputLayer.Controls:
    //                updateControlsLayer();
    //                break;
    //            case InputLayer.Settings:
    //                updateSettingsLayer();
    //                break;
    //            case InputLayer.Dialogue:
    //                updateDialogueLayer();
    //                break;
    //           case InputLayer.ChooseDialogueOption:
    //                updateChooseDialogueOptionLayer();
    //                break;
    //            case InputLayer.ChoosePreCombatOption:
    //                updateChoosePreCombatOptionLayer();
    //                break;
    //            case InputLayer.ChooseCombatOption:
    //                updateChooseCombatOptionLayer();
    //                break;
    //            case InputLayer.ChooseItemOption:
    //                updateChooseItemOptionLayer();
    //                break;
    //        }
    //    }

    //     Handle Inputs for SplashScreen if any
    //    private void updateSplashScreenLayer()
    //    {
    //         Skip splash screen
    //        if (Input.GetKeyDown(KeyCode.Escape))
    //        {
    //            _gameManager.ChangeState(GameState.MainMenu);
    //        }
    //    }

    //     Handle Inputs for MainMenu if any
    //    private void updateMainMenuLayer()
    //    {
    //         Only Take Input for Up/Down Arrow Key and Return Key
    //        if (selectionUp())
    //        {
    //              _gameManager.UpdateMainMenuGUI();
    //        }
    //        if (selectionDown())
    //        {
    //               _gameManager.UpdateMainMenuGUI();
    //        }
    //        if (selectionConfirm())
    //        {
    //            if (SelectedItemIndex == 0)
    //            {
    //                 Start
    //                _gameManager.ChangeState(GameStateType.PreGame);
    //            }
    //            else if (SelectedItemIndex == 1)
    //            {
    //                 Controls
    //                _gameManager.ChangeState(GameStateType.Controls);
    //            }
    //            else if (SelectedItemIndex == 2)
    //            {
    //                 Settings
    //                _gameManager.ChangeState(GameStateType.Settings);
    //            }
    //            else if (SelectedItemIndex == 3)
    //            {
    //                 Exit
    //                Application.Quit();
    //            }
    //        }
    //    }

    //     Handle Inputs for Controls if any
    //    private void updateControlsLayer()
    //    {
    //        if (goBack())
    //        {
    //            _gameManager.ChangeState(GameStateType.MainMenu);
    //        }
    //    }

    //     Handle Inputs for Settings if any
    //    private void updateSettingsLayer()
    //    {
    //         Only Take Input for Arrow Keys and Return Key
    //        if (goBack())
    //        {
    //            _gameManager.ChangeState(GameStateType.MainMenu);
    //        }

    //         Up/Down for Setting Options
    //        if (selectionUp() || selectionDown())
    //        {
    //            if (SelectedItemIndex == 0)
    //            {
    //                 BGM Volume
    //                SetAdjustmentLimit(_settingManager.MaxBGMVolumeValue, _settingManager.MinBGMVolumeValue);
    //                SetAdjustmentRatio(_gameManager.SettingManager.BGMVolumeAdjustmentRatio);
    //                AdjustmentValue = _gameManager.SettingManager.BGMVolume;
    //            }
    //            else if (SelectedItemIndex == 1)
    //            {
    //                 SFX Volume
    //                SetAdjustmentLimit(_settingManager.MaxSFXVolumeValue, _settingManager.MinSFXVolumeValue);
    //                SetAdjustmentRatio(_gameManager.SettingManager.SFXVolumeAdjustmentRatio);
    //                AdjustmentValue = _gameManager.SettingManager.SFXVolume;
    //            }
    //            else if (SelectedItemIndex == 2)
    //            {
    //                 Narrator Volume
    //                SetAdjustmentLimit(_settingManager.MaxNarratorVolume, _settingManager.MinNarratorVolume);
    //                SetAdjustmentRatio(_gameManager.SettingManager.NarratorVolumeAdjustmentRatio);
    //                AdjustmentValue = _gameManager.SettingManager.NarratorVolume;
    //            }
    //        }

    //         Left/Right for value
    //        if (adjustIncrease() || adjustDecrease())
    //        {
    //            if (SelectedItemIndex == 0)
    //            {
    //                 BGM Volume
    //                _gameManager.SettingManager.BGMVolume = AdjustmentValue;
    //            }
    //            else if (SelectedItemIndex == 1)
    //            {
    //                 SFX Volume
    //                _gameManager.SettingManager.SFXVolume = AdjustmentValue;
    //            }
    //            else if (SelectedItemIndex == 2)
    //            {
    //                 Narrator Volume
    //                _gameManager.SettingManager.NarratorVolume = AdjustmentValue;
    //            }
    //            else if (SelectedItemIndex == 3)
    //            {
    //                 Narrator Speed
    //                _gameManager.Narrator.FastForward();
    //                _gameManager.SettingManager.NarratorSpeed = AdjustmentValue;
    //            }
    //        }
    //    }

    //     Handle Inputs for Dialogue if any
    //    private void updateDialogueLayer()
    //    {
    //         Skip
    //        if (goBack())
    //        {
    //            _gameManager.Narrator.Skip();
    //        }
    //         Fast forward
    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            _gameManager.Narrator.FastForward();
    //            _gameManager.CurrentGameState.UpdateGUI();

    //        }
    //         Replay
    //        if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            _gameManager.Narrator.Replay(PlayType.Dialogue);
    //        }
    //    }

    //     Handle Inputs for ChooseDialogueOption if any
    //    private void updateChooseDialogueOptionLayer()
    //    {
    //         Choose Dialogue Option
    //        if (selectionUp())
    //        {
    //            _gameManager.CurrentGameState.UpdateGUI();
    //        }
    //        if (selectionDown())
    //        {
    //            _gameManager.CurrentGameState.UpdateGUI();
    //        }
    //         Confirm Dialogue Option
    //        if (selectionConfirm())
    //        {
    //            _gameManager.NextEvent();
    //        }
    //         Fast forward
    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            _gameManager.Narrator.FastForward();
    //            _gameManager.CurrentGameState.UpdateGUI();
    //        }

    //         Replay Option
    //        if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            _gameManager.Narrator.Replay(PlayType.Option);
    //        }

    //         Replay Dialogue
    //        if (Input.GetKeyDown(KeyCode.T))
    //        {
    //            _gameManager.Narrator.Replay(PlayType.Dialogue);
    //        }
    //    }

    //     Handle Inputs for ChoosePreCombatOption if any
    //    private void updateChoosePreCombatOptionLayer()
    //    {
    //        if (selectionUp())
    //        {
    //            _gameManager.BattleState.UpdatePlayerTurnGUI();

    //            if (_gameManager.ClipPreCombatOptions[SelectedItemIndex] != null)
    //            {
    //                _gameManager.Narrator.Play(_gameManager.ClipPreCombatOptions[SelectedItemIndex]);
    //            }

    //        }

    //        if (selectionDown())
    //        {
    //            _gameManager.BattleState.UpdatePlayerTurnGUI();


    //            if (_gameManager.ClipPreCombatOptions[SelectedItemIndex] != null)
    //            {
    //                _gameManager.Narrator.Play(_gameManager.ClipPreCombatOptions[SelectedItemIndex]);
    //            }

    //        }

    //        if (selectionConfirm())
    //        {
    //            _gameManager.BattleState.SelectPreCombatOption(SelectedItemIndex);
    //            _gameManager.CurrentGameState.UpdateGUI();
    //        }
    //    }

    //     Handle Inputs for ChooseCombatOption if any
    //    private void updateChooseCombatOptionLayer()
    //    {
    //        if (selectionUp())
    //        {
    //            _gameManager.BattleState.UpdatePlayerTurnGUI();


    //            if (_gameManager.ClipCombatAttackOptions[SelectedItemIndex] != null)
    //            {
    //                _gameManager.Narrator.Play(_gameManager.ClipCombatAttackOptions[SelectedItemIndex]);
    //            }
    //        }

    //        if (selectionDown())
    //        {
    //            _gameManager.BattleState.UpdatePlayerTurnGUI();

    //            if (_gameManager.ClipCombatAttackOptions[SelectedItemIndex] != null)
    //            {
    //                _gameManager.Narrator.Play(_gameManager.ClipCombatAttackOptions[SelectedItemIndex]);
    //            }
    //        }

    //        if (selectionConfirm())
    //        {
    //            _gameManager.BattleState.SelectCombatOption(SelectedItemIndex);
    //            _gameManager.CurrentGameState.UpdateGUI();
    //        }
    //    }

    //     Handle Inputs for ChooseItemOption if any
    //    private void updateChooseItemOptionLayer()
    //    {
    //        if (goBack())
    //        {
    //            _gameManager.BattleState.PlayerSelectingStatus = PlayerSelectingStatus.NoSelection;
    //            ChangeInputLayer(InputLayer.ChoosePreCombatOption, _gameManager.ClipPreCombatOptions);
    //        }
    //        if (selectionUp())
    //        {
    //            _gameManager.BattleState.UpdatePlayerTurnGUI();

    //        }

    //        if (selectionDown())
    //        {
    //            _gameManager.BattleState.UpdatePlayerTurnGUI();
    //        }
    //        if (selectionConfirm())
    //        {
    //            _gameManager.BattleState.SelectCombatOption(SelectedItemIndex);
    //            _gameManager.CurrentGameState.UpdateGUI();
    //        }
    //    }

    //public void ChangeInputLayer(InputLayer layer, AudioClip[] clips)
    //{
    //    InputLayer = layer;
    //    //_clipMenuItems = clips;
    //    setMaxItemCount(clips.Length);
    //}

    //public void SetAdjustmentLimit(float max, float min)
    //{
    //    if (max > min)
    //    {
    //        MaxAdjustmentValue = max;
    //        MinAdjustmentValue = min;
    //    }
    //    else
    //    {
    //        MaxAdjustmentValue = min;
    //        MinAdjustmentValue = max;
    //    }
    //}

    //public void SetAdjustmentRatio(float ratio)
    //{
    //    if (ratio > 0 && ratio < MaxAdjustmentValue)
    //    {
    //        AdjustmentRatio = ratio;
    //    }
    //}

    public void ResetSelection()
    {
        SelectionStatus = SelectionStatus.None;
    }

    public void SetMenuItemLimit(int count)
    {
        // Only set MaxItemCount to count if the item count is greater than 1, and set the default SelectedItemIndex to 0
        if (count > 1)
        {
            MenuItemSelectionLimit = count;
            SelectedItemIndex = 0;
        }
        else
        {
            MenuItemSelectionLimit = 0;
            SelectedItemIndex = -1;
        }
    }

    public bool SelectionUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (SelectedItemIndex - 1 < 0)
            {
                SelectedItemIndex = MenuItemSelectionLimit - 1;
            }
            else
            {
                SelectedItemIndex--;
            }
            if (SFX[0] != null)
            {
                _gm.AudioManager.Play(SFX[0], AudioChannel.SFX1);
            }
            return true;
        }
        return false;
    }

    public bool SelectionDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (SelectedItemIndex + 1 >= MenuItemSelectionLimit)
            {
                SelectedItemIndex = 0;
            }
            else
            {
                SelectedItemIndex++;
            }
            if (SFX[0] != null)
            {
                _gm.AudioManager.Play(SFX[0], AudioChannel.SFX1);
            }
            return true;
        }
        return false;
    }

    //    private bool adjustIncrease()
    //    {
    //        if (Input.GetKeyDown(KeyCode.RightArrow))
    //        {
    //            if (AdjustmentValue + AdjustmentRatio < MaxAdjustmentValue)
    //            {
    //                AdjustmentValue += AdjustmentRatio;
    //            }
    //            else
    //            {
    //                AdjustmentValue = MaxAdjustmentValue;
    //            }
    //            _gameManager.CurrentGameState.UpdateGUI();
    //            return true;
    //        }
    //        return false;
    //    }

    //    private bool adjustDecrease()
    //    {
    //        if (Input.GetKeyDown(KeyCode.LeftArrow))
    //        {
    //            if (AdjustmentValue - AdjustmentRatio > MinAdjustmentValue)
    //            {
    //                AdjustmentValue -= AdjustmentRatio;
    //            }
    //            else
    //            {
    //                AdjustmentValue = MinAdjustmentValue;
    //            }
    //            _gameManager.AudioManager.PlaySFX(_gameManager.AudioAssets.SFXMenu[0]);
    //            _gameManager.CurrentGameState.UpdateGUI();
    //            return true;
    //        }
    //        return false;
    //    }

    public bool SelectionConfirm()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (SFX[1] != null)
            {
                _gm.AudioManager.Play(SFX[1], AudioChannel.SFX1);
            }
            return true;
        }
        return false;
    }

    public bool FastForward()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            return true;
        }
        return false;
    }

    public bool SelectionSkipOrExit(bool skipDialogue)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SFX[2] != null && skipDialogue)
            {
                _gm.AudioManager.Play(SFX[2], AudioChannel.SFX1);
            }
            return true;
        }
        return false;
    }

    //    private bool goBack()
    //    {
    //        if (Input.GetKeyDown(KeyCode.Escape))
    //        {
    //             ToDo: Play back SFX
    //            return true;
    //        }
    //        return false;
    //    }
    //    public bool InputCompleted
    //    {
    //        get
    //        {
    //            return InputStatus == InputStatus.Completed;
    //        }
    //    }

    //    public bool InputCompletedOrIsValid
    //    {
    //        get
    //        {
    //            return (InputStatus == InputStatus.Completed || SelectionStatus == SelectionStatus.Valid);
    //        }
    //    }

    //    public bool ValidInput
    //    {
    //        get
    //        {
    //            return SelectionStatus == SelectionStatus.Valid;
    //        }
    //    }
}
