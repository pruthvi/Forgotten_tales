using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Forgotten Tale/Combat/Spell")]
public class Spell : ScriptableObject
{
    public string Name;
    public float BaseDamage = 1;
    public float DamageModifier = 1;
    public AudioClip sfxSoundOnFire;
    public AudioClip sfxSoundOnAttacked;

    public float TotalDamage
    {
        get
        {
            return BaseDamage * DamageModifier;
        }
    }

    public void SetDamageModifier(float modifier)
    {
        DamageModifier = modifier < 0 ? modifier : 1;
    }
}
