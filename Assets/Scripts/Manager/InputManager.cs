using System.Collections;
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

    private GameManager _gameManager;

    public InputManager(GameManager gameManager)
    {
        _gameManager = gameManager;
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectionUp();
            _gameManager.UpdateMainMenuGUI();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectionDown();
            _gameManager.UpdateMainMenuGUI();
        }
        if (Input.GetKeyDown(KeyCode.Return))
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
            else if (SelectedItemIndex == 2)
            {
                // Exit
                Application.Quit();
            }
            _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXConfirm);
        }
    }

    // Handle Inputs for Controls if any
    private void updateControlsLayer()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameManager.ChangeState(GameState.MainMenu);
        }
    }

    // Handle Inputs for Settings if any
    private void updateSettingsLayer()
    {
        // Only Take Input for Arrow Keys and Return Key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameManager.ChangeState(GameState.MainMenu);
        }
    }

    // Handle Inputs for Dialogue if any
    private void updateDialogueLayer()
    {
        // Skip
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameManager.Narrator.Skip();
            _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXSkip);
        }
        // Fast forward
        if (Input.GetKeyDown(KeyCode.F))
        {
            _gameManager.Narrator.FastForward();
            _gameManager.UpdateInGameGUI();
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectionUp();
            _gameManager.UpdateDialogueWithOptionGUI();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectionDown();
            _gameManager.UpdateDialogueWithOptionGUI();
        }
        // Confirm Dialogue Option
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_gameManager.CurrentAct.NextDialogue(SelectedItemIndex))
            {
                InputStatus = InputStatus.Completed;
                SelectionStatus = SelectionStatus.Valid;
                _gameManager.Narrator.SetToIdle();
                _gameManager.GameProgress = GameProgress.Dialogue;
                _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXConfirm);
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

    public void ChangeInputLayer(InputLayer layer)
    {
        this.InputLayer = layer;
    }

    public void ChangeInputLayer(InputLayer layer, int maxItemCount)
    {
        this.InputLayer = layer;
        SetMaxItemCount(maxItemCount);
    }

    public void ResetSelection()
    {
        this.InputStatus = InputStatus.Idle;
        this.SelectionStatus = SelectionStatus.None;
    }

    public void SetMaxItemCount(int count)
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

    private void selectionUp()
    {
        if (SelectedItemIndex - 1 < 0)
        {
            SelectedItemIndex = MaxItemCount - 1;
        }
        else
        {
            SelectedItemIndex--;
        }
        _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXMenuSelection);
    }

    private void selectionDown()
    {
        if (SelectedItemIndex + 1 >= MaxItemCount)
        {
            SelectedItemIndex = 0;
        }
        else
        {
            SelectedItemIndex++;
        }
        _gameManager.AudioManager.PlaySFX(_gameManager.AudioManager.SFXMenuSelection);
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
