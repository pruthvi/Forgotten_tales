using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ControlsState : GameState
{
    public ControlsState(GameManager gm) : base(gm)
    {

    }

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.Controls;
        }
    }

    public override void UpdateGUI()
    {
        _gameManager.TextUI.text = "Controls\n[Esc] Back to Menu";
        _gameManager.TextDescription.text = "";
    }

    public override void OnStateEnter()
    {
        _gameManager.InputManager.ChangeInputLayer(InputLayer.Controls, 0);
        //StartCoroutine(playControlsAudio());
        UpdateGUI();
        _gameManager.TextDescription.alignment = TextAnchor.MiddleCenter;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {

    }
}
