using System;
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

    public override void Attack(Combatant target, Spell spell)
    {
        GameManager.Instance.AudioManager.Play(spell.SFXWhenFire, AudioChannel.SFX1);
        target.OnHit(this, spell);
    }

    public override void OnHit(Combatant attacker, Spell spell)
    {
        //Play goblin getting hit sfx
        GameManager.Instance.AudioManager.Play(spell.SFXWhenHit, AudioChannel.SFX2);
        this.HP -= spell.FinalDamage;
    }
}
