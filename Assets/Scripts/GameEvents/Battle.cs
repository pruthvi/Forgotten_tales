using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum BattleResult { None, Win, Lose }
[CreateAssetMenu(menuName = "Forgotten Tale/Game Event/Battle")]
public class Battle : GameEvent
{
    public Combatant Initiator;
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

    public GameEvent WinEvent;
    public GameEvent LoseEvent;

    public void InitiateFight(Combatant attacker, Combatant defender, Spell spell)
    {
        attacker.Attack(defender, spell);
    }

    public void EndBattle()
    {
    }
}