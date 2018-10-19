using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public enum InputLayer { SplashScreen, MainMenu, Controls, Settings, Dialogue, ChooseDialogueOption, ChoosePreCombatOption, ChooseCombatOption, ChooseItemOption }
public enum InputStatus { Idle, Selecting, Completed }
public enum SelectionStatus { None, Valid, Invalid }

public class InputManager
{
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

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

    public void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }
        prevState = state;
        state = GamePad.GetState(playerIndex);

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
            case InputLayer.ChooseItemOption:
                updateChooseItemOptionLayer();
                break;
        }
    }

    // Handle Inputs for SplashScreen if any
    private void updateSplashScreenLayer()
    {
        // Skip splash screen
        //if ((Input.GetKeyDown(KeyCode.Escape)) || (state.Buttons.Back == ButtonState.Pressed && prevState.Buttons.Back == ButtonState.Released))
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
            //  _gameManager.UpdateMainMenuGUI();
        }
        if (selectionDown())
        {
            //   _gameManager.UpdateMainMenuGUI();
        }
        if (selectionConfirm())
        {
            if (SelectedItemIndex == 0)
            {
                // Start
                _gameManager.ChangeState(GameStateType.PreGame);
            }
            else if (SelectedItemIndex == 1)
            {
                // Controls
                _gameManager.ChangeState(GameStateType.Controls);
            }
            else if (SelectedItemIndex == 2)
            {
                // Settings
                _gameManager.ChangeState(GameStateType.Settings);
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
            _gameManager.ChangeState(GameStateType.MainMenu);
        }
    }
    
    // Handle Inputs for Settings if any
    private void updateSettingsLayer()
    {
        // Only Take Input for Arrow Keys and Return Key
        if (goBack())
        {
            _gameManager.ChangeState(GameStateType.MainMenu);
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
        if ((Input.GetKeyDown(KeyCode.F)) ||
            (state.Buttons.RightShoulder == ButtonState.Pressed && prevState.Buttons.RightShoulder == ButtonState.Released))
        {
            _gameManager.Narrator.FastForward();
            _gameManager.CurrentGameState.UpdateGUI();
            _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXMenuItemSelection);

        }
        // Replay
        if ((Input.GetKeyDown(KeyCode.R)) || (state.Triggers.Left == 1))
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
            _gameManager.CurrentGameState.UpdateGUI();
        }
        if (selectionDown())
        {
            _gameManager.CurrentGameState.UpdateGUI();
        }
        // Confirm Dialogue Option
        if (selectionConfirm())
        {
            _gameManager.NextEvent();
        }
        // Fast forward
        if ((Input.GetKeyDown(KeyCode.F)) ||
            (state.Buttons.RightShoulder == ButtonState.Pressed && prevState.Buttons.RightShoulder == ButtonState.Released))
        {
            _gameManager.Narrator.FastForward();
            _gameManager.CurrentGameState.UpdateGUI();
        }

        // Replay Option
        if ((Input.GetKeyDown(KeyCode.R)) || (state.Triggers.Left == 1))
        {
            _gameManager.Narrator.Replay(PlayType.Option);
        }

        // Replay Dialogue
        if ((Input.GetKeyDown(KeyCode.T)) || (state.Buttons.LeftShoulder == ButtonState.Pressed && prevState.Buttons.LeftShoulder == ButtonState.Released))
        {
            _gameManager.Narrator.Replay(PlayType.Dialogue);
        }
    }
    
    // Handle Inputs for ChoosePreCombatOption if any
    private void updateChoosePreCombatOptionLayer()
    {
        if (selectionUp())
        {
            _gameManager.CurrentGameState.UpdateGUI();
        }

        if (selectionDown())
        {
            _gameManager.CurrentGameState.UpdateGUI();
        }

        if (selectionConfirm())
        {
            if (SelectedItemIndex == 0)
            {
                // Attack
                Battle currentBattle = _gameManager.CombatManager.CurrentBattle;
              //  currentBattle.InitiateFight(_gameManager.Player, currentBattle.Mob);
            }
            else if (SelectedItemIndex == 1)
            {
                // Choose Item
                ChangeInputLayer(InputLayer.ChooseItemOption, _gameManager.Player.InventoryManager.Items.Count);
            }
            else if (SelectedItemIndex == 2)
            {
                // Choose Run
                _gameManager.Player.Choice = PlayerChoice.Attack;
            }
        }
    }

    // Handle Inputs for ChooseCombatOption if any
    private void updateChooseCombatOptionLayer()
    {

    }

    // Handle Inputs for ChooseItemOption if any
    private void updateChooseItemOptionLayer()
    {

    }

    public void ChangeInputLayer(InputLayer layer, int maxItemCount)
    {
        InputLayer = layer;
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
        InputStatus = InputStatus.Idle;
        SelectionStatus = SelectionStatus.None;
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
        if ((Input.GetKeyDown(KeyCode.UpArrow)) ||
            (state.DPad.Up == ButtonState.Pressed && prevState.DPad.Up == ButtonState.Released))
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
            _gameManager.CurrentGameState.UpdateGUI();
            return true;
        }
        return false;
    }

    private bool selectionDown()
    {
        if ((Input.GetKeyDown(KeyCode.DownArrow)) ||
            (state.DPad.Down == ButtonState.Pressed && prevState.DPad.Down == ButtonState.Released))
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
            _gameManager.CurrentGameState.UpdateGUI();
            return true;
        }
        return false;
    }

    private bool adjustIncrease()
    {
        if ((Input.GetKeyDown(KeyCode.RightArrow)) ||
                (state.DPad.Right == ButtonState.Pressed && prevState.DPad.Right == ButtonState.Released))
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
            _gameManager.CurrentGameState.UpdateGUI();
            return true;
        }
        return false;
    }

    private bool adjustDecrease()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow)) ||
            (state.DPad.Left == ButtonState.Pressed && prevState.DPad.Left == ButtonState.Released))
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
            _gameManager.CurrentGameState.UpdateGUI();
            return true;
        }
        return false;
    }

    private bool selectionConfirm()
    {
        if ((Input.GetKeyDown(KeyCode.Return)) ||
            (state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released))
        {
            _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXConfirm);
            return true;
        }
        return false;
    }

    private bool goBack()
    {
        if ((Input.GetKeyDown(KeyCode.Escape)) ||
            (state.Buttons.Back == ButtonState.Pressed && prevState.Buttons.Back == ButtonState.Released))
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
