using UnityEngine;

public enum PlayerChoice { Idle, Fight, Attack, Spell, Defense, Item, Run }
[CreateAssetMenu(menuName = "Forgotten Tale/Combat/Player")]
public class Player : Combatant {
    
    public InventoryManager InventoryManager = new InventoryManager();

    public PlayerChoice Choice;

    public override CombatantType CombatantType
    {
        get
        {
            return CombatantType.Player;
        }
    }

    public override void Attack(Combatant target, Spell spell)
    {
        GameManager.Instance.AudioManager.PlaySFX(spell.SFXSoundOnFire);
        target.OnHit(this, spell);
    }

    public override void OnHit(Combatant attacker, Spell spell)
    {
        if (Defense)
        {
            float rng = Random.Range(0, 1);
            if (rng < 0.2)
            {
                // Block attack
                //GameManager.Instance.AudioManager.PlaySFX(GameManager.Instance.AudioBattles[]);
            }
            else
            {
                GameManager.Instance.AudioManager.PlaySFX2(spell.SFXSoundOnAttacked);
            }
            Defense = false;
        }
        else
        {
            this.HP -= spell.FinalDamage;
            GameManager.Instance.AudioManager.PlaySFX2(spell.SFXSoundOnAttacked);
        }
        
    }
}
