using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CombatantInfo
{
    public int HealthPoint { get; set; }
    public int MagicPoint { get; set; }

    public CombatantInfo(int hp, int mp)
    {
        this.HealthPoint = hp;
        this.MagicPoint = mp;
    }
}
