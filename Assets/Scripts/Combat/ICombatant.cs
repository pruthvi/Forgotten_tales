using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum CombatantType { Player, AI }

public interface ICombatant
{
    string Name { get; set; }
    CombatantType CombatantType { get; }
    CombatantInfo CombatantInfo { get; }

    void Cast(Spell spell, ICombatant target);
    void Attack(ICombatant target);
    void Defense();
}