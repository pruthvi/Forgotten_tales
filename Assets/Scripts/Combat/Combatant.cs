﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum CombatantType { Player, Mob }

public abstract class Combatant : ScriptableObject
{
    public string Name { get; set; }
    public abstract CombatantType CombatantType { get; }
    
    public abstract void Attack(Combatant target, Spell spell);

    public Spell[] Spells;

    private float _maxHp;
    private float _maxMp;

    private float _hp;
    private float _mp;
    public bool Defense;

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
                _hp = _maxHp;
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
                _mp = _maxMp;
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

    public abstract void OnHit(Combatant attacker, Spell spell);
}