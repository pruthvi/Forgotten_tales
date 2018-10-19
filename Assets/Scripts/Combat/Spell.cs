using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Forgotten Tale/Combat/Spell")]
public class Spell : ScriptableObject
{
    public string Name;
    private float _baseDamage = 1;
    public float BaseDamage
    {
        get
        {
            return _baseDamage;
        }
        set
        {
            if (value < 1)
            {
                _baseDamage = 1;
            }
            else
            {
                _baseDamage = value;
            }
        }
    }
    public float DamageModifier = 1;
    public AudioClip SFXSoundOnFire;
    public AudioClip SFXSoundOnAttacked;

    public float FinalDamage
    {
        get
        {
            return BaseDamage * DamageModifier;
        }
    }
}
