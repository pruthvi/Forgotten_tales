using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MainMenuState : GameState
{

    private string[] _menuItems = { "Start Game", "Controls", "Settings", "Exit" };

    public MainMenuState(GameManager gm) : base(gm)
    {

    }

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.MainMenu;
        }
    }

    public override void UpdateGUI()
    {
        // Display each MenuItem
        string menu = "";
        for (int i = 0; i < _menuItems.Length; i++)
        {
            // Put > infront of the MenuItem if it was the SelectedIndex
            menu += (_gameManager.InputManager.SelectedItemIndex == i ? "> " : "\t") + _menuItems[i] + "\n";
        }
        _gameManager.TextDescription.text = menu;
    }

    public override void OnStateEnter()
    {
        _gameManager.InputManager.ChangeInputLayer(InputLayer.MainMenu, _menuItems.Length);
        _gameManager.AudioManager.PlayBGM(_gameManager.AudioManager.BGMMainMenuToIntro);
        _gameManager.TextUI.text = "";
        _gameManager.TextDescription.alignment = TextAnchor.MiddleCenter;
        UpdateGUI();
        _gameManager.Narrator.Play(_gameManager.ClipMainMenuGuide[0]);
        //_gameManager.StartCoroutine(playMainMenuEnterAudio());
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }

    //IEnumerator playMainMenuEnterAudio()
    //{
    //    for (int i = 0; i < _gameManager.ClipMainMenuGuide.Length; i++)
    //    {
    //        _gameManager.Narrator.Play(_gameManager.ClipMainMenuGuide[i]);
    //        yield return new WaitForSeconds(_gameManager.ClipMainMenuGuide[i].length * (1 / _gameManager.Narrator.Speed));
    //    }
    //}
}