using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CombatantInfo
{
    public int HealthPoint { get; private set; }
    public int ManaPoint { get; private set; }

    public CombatantInfo(int hp, int mp)
    {
        this.HealthPoint = hp;
        this.ManaPoint = mp;
    }

    public void Heal()
    {

    }

    public void Hit()
    {

    }
}
