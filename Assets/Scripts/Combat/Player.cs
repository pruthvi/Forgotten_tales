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

    public override void Cast(Spell spell, Combatant target)
    {
        throw new NotImplementedException();
    }

    public override void Attack(Combatant target)
    {
        throw new NotImplementedException();
    }

    public override void Defense()
    {
        throw new NotImplementedException();
    }
}
