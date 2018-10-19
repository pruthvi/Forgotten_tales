using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum BattleResult { Win, Lose }
[CreateAssetMenu(menuName = "Forgotten Tale/Game Event/Battle")]
public class Battle : GameEvent
{
    public bool Initiator;
    public Enemy[] Enemies;
    [HideInInspector]
    public BattleResult BattleResult;

    public override GameEventType GameEventType
    {
        get
        {
            return GameEventType.Battle;
        }
    }

    public override GameEvent DefaultEvent
    {
        get
        {
            if (BattleResult == BattleResult.Win)
            {
                return WinEvent;
            }
            else if(BattleResult == BattleResult.Lose)
            {
                return LoseEvent;
            }
            else
            {
                return base.DefaultEvent;
            }
        }
    }

    public GameEvent WinEvent;
    public GameEvent LoseEvent;

    public void End()
    {

    }
}