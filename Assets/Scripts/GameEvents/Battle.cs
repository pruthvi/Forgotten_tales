using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum BattleResult { None, Win, Lose }
[CreateAssetMenu(menuName = "Forgotten Tale/Game Event/Battle")]
public class Battle : GameEvent
{
    private BattleTurn BattleTurn;

    private Player _player;

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

    public override void OnEnter()
    {
        _gm = GameManager.Instance;

        _gm.UIManager.Header = "";
        _gm.UIManager.Content = "";

        Init();


        OnGUIChange();
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnGUIChange()
    {
        updatePlayerStatusGUI();
        updateEnemiesStatusGUI();
    }

    private void updatePlayerStatusGUI()
    {
        _gm.UIManager.Header = _player.Name + ": " + _player.HP + "/" + _player.MaxHP + "|" + _player.MP + "/" + _player.MaxMP;
    }

    private void updateEnemiesStatusGUI()
    {

    }

    public override void OnInput()
    {
        
    }

    public override void Init()
    {
        _player = _gm.InGameState.Player;
        _player.HP = _player.MaxHP;
        _player.MP = _player.MaxMP;

        foreach (Enemy e in Enemies)
        {
            e.HP = e.MaxHP;
            e.MP = e.MaxMP;
        }


        if (Initiator.CombatantType == CombatantType.Mob)
        {
            BattleTurn = BattleTurn.Mob;
        }
        else
        {
            BattleTurn = BattleTurn.Player;
        }
    }
}