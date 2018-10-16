using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ICombatant {

    public string Name { get; set; }
    public CombatantInfo CombatantInfo { get; set; }
    public InventoryManager InventoryManager { get; private set; }

    public CombatantType CombatantType
    {
        get
        {
            return CombatantType.Player;
        }
    }

    public Player(string name, int hp, int mp)
    {
        this.Name = name;
        this.InventoryManager = new InventoryManager();
        CombatantInfo = new CombatantInfo(hp, mp);
    }

    public void Cast(Spell spell, ICombatant target)
    {
        throw new System.NotImplementedException();
    }

    public void Attack(ICombatant target)
    {
        throw new System.NotImplementedException();
    }

    public void Defense()
    {
        throw new System.NotImplementedException();
    }
}
