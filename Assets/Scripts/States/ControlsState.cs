using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ControlsState : GameState
{

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.Controls;
        }
    }

    public override void OnGUIChange()
    {
        _gm.UIManager.Header = "Controls\n[Esc] Back to Menu | [Up/Down]";
        _gm.UIManager.Content = "";
    }

    public override void OnEnter()
    {
        _gm.UIManager.TextContent.alignment = TextAnchor.MiddleCenter;
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
