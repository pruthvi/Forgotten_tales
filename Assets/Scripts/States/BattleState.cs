using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BattleState : GameState
{
    private Battle _battle;

    public BattleState(GameManager gm) : base(gm)
    {

    }

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.Battle;
        }
    }

    public override void UpdateGUI()
    {
        
    }

    public override void OnStateEnter()
    {
        _battle = _gameManager.CombatManager.CurrentBattle;
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}