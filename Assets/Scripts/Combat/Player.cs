using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Forgotten Tale/Combat/Player")]
public class Player : Combatant {
    
    public InventoryManager InventoryManager = new InventoryManager();

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

    public override void Defense()
    {
        _defense = true;
    }

    public override void OnHit(Combatant attacker, Spell spell)
    {
        if (_defense)
        {
            //playe player block attack sfx
            _defense = false;
        }
        else
        {
            //play player getting hit sfx
        }
    }
}
