using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Spell
{
    public float BaseDamage { get; private set; }

    public float DamageModifier { get; private set; }

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
