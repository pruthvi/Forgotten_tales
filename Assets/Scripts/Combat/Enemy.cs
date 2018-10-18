using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ScriptableObject, ICombatant {
    public string Name
    {
        get; set;
    }

    public CombatantInfo CombatantInfo { get; private set; }

    public CombatantType CombatantType
    {
        get
        {
            return CombatantType.AI;
        }
    }

    public Enemy(string name, int hp, int mp)
    {
        this.Name = name;
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
