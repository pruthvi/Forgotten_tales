using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MainMenuState : GameState
{
    public AudioClip[] MainMenuItemClips;
    public string[] MenuItemsText;

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.MainMenu;
        }
    }

    public override void OnGUIChange()
    {
        // Display each MenuItem
        string menu = "";
        for (int i = 0; i < MenuItemsText.Length; i++)
        {
            // Put > infront of the MenuItem if it was the SelectedIndex
            menu += (_gm.InputManager.SelectedItemIndex == i ? "> " : "\t") + MenuItemsText[i] + "\n";
        }
        _gm.UIManager.TextContent.text = menu;
    }

    public override void OnEnter()
    {
        _gm.AudioManager.Play(_gm.AudioManager.BGMMainMenuToIntro, AudioChannel.BGM);
        _gm.UIManager.Header = "";
        _gm.UIManager.TextContent.alignment = TextAnchor.MiddleCenter;
        _gm.InputManager.SetMenuItemLimit(MenuItemsText.Length);
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
        if (_gm.InputManager.SelectionUp())
        {
            _gm.AudioManager.Play(MainMenuItemClips[_gm.InputManager.SelectedItemIndex], AudioChannel.SFX2);
            OnGUIChange();
        }

        if (_gm.InputManager.SelectionDown())
        {
            _gm.AudioManager.Play(MainMenuItemClips[_gm.InputManager.SelectedItemIndex], AudioChannel.SFX2);
            OnGUIChange();
        }

        if (_gm.InputManager.SelectionConfirm())
        {
            int index = _gm.InputManager.SelectedItemIndex;

            if (index == 0)
            {
                _gm.ChangeState(GameStateType.PreGame);
            }
            else if (index == 1)
            {
                _gm.ChangeState(GameStateType.Controls);
            }
            else if (index == 2)
            {
                _gm.ChangeState(GameStateType.Setting);
            }
            else if (index == 3)
            {
                Application.Quit();
            }
        }
    }
    
}