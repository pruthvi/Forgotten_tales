using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerChoice { Idle, Attack, Item, Run }
[CreateAssetMenu(menuName = "Forgotten Tale/Combat/Player")]
public class Player : Combatant {
    
    public InventoryManager InventoryManager = new InventoryManager();

    public PlayerChoice Choice;

    public override CombatantType CombatantType
    {
        get
        {
            return CombatantType.Player;
        }
    }

    public override void Attack(Combatant target, Spell spell)
    {
        target.OnHit(this, spell);
    }

    public override void OnHit(Combatant attacker, Spell spell)
    {
        if (Defense)
        {
            //playe player block attack sfx
            Defense = false;
        }
        else
        {
            //play player getting hit sfx
        }
    }
}
