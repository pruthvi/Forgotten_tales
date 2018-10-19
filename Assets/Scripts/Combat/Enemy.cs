﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Forgotten Tale/Combat/Enemy")]
public class Enemy : Combatant {

    public override CombatantType CombatantType
    {
        get
        {
            return CombatantType.Mob;
        }
    }

    public Spell[] Spells;

    public override void Attack(Combatant target, Spell spell)
    {
        
    }

    public override void Defense()
    {
        throw new NotImplementedException();
    }

    public override void OnHit(Combatant attacker, Spell spell)
    {
        //Play goblin getting hit sfx
    }
}
