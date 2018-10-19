using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum CombatantType { Player, Mob }

public abstract class Combatant : ScriptableObject
{
    public string Name { get; set; }
    public abstract CombatantType CombatantType { get; }

    public abstract void Cast(Spell spell, Combatant target);
    public abstract void Attack(Combatant target);
    public abstract void Defense();

    private float _hp;
    private float _mp;
    private float _maxHp;
    private float _maxMp;

    public float MaxHP
    {
        get
        {
            return _maxHp;
        }
        set
        {
            if (value > 0)
            {
                _maxHp = value;
            }
            else
            {
                _maxHp = 0;
            }

            if (_hp > _maxHp)
            {
                _hp = _maxHp;
            }
        }
    }
    public float MaxMP
    {
        get
        {
            return _maxMp;
        }
        set
        {
            if (value > 0)
            {
                _maxMp = value;
            }
            else
            {
                _maxMp = 0;
            }

            if (_mp > _maxMp)
            {
                _mp = _maxMp;
            }
        }
    }
    public float HP
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value > MaxHP)
            {
                _hp = MaxHP;
            }
            else if(value < 0)
            {
                _hp = 0;
            }
            else
            {
                _hp = value;
            }
        }
    }
    public float MP
    {
        get
        {
            return _mp;
        }
        set
        {
            if (value > MaxMP)
            {
                _mp = MaxMP;
            }
            else if (value < 0)
            {
                _mp = 0;
            }
            else
            {
                _mp = value;
            }
        }
    }
}