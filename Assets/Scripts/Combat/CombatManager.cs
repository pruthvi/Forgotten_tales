using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatResult { None, AttackerWon, AttackerLost }
public enum CombatStatus { Start, InProgress, End }
public enum CombatTurn { Attacker, Defender }
public enum CombatAction { Idle, CastSpell, Attack, UseItem, Defense, Run }
public class CombatManager
{

    public CombatResult CombatResult;
    public CombatStatus CombatStatus;
    public CombatTurn CombatTurn;
    public ICombatant Attacker { get; private set; }
    public ICombatant Defender { get; private set; }
    public ICombatant CurrentCombatant { get; private set; }
    public bool TurnEnds { get; private set; }

    event EventHandler combatEventHandler;

    public CombatManager()
    {
        this.CombatResult = CombatResult.None;
        TurnEnds = false;
    }

    //public void CombatUpdate()
    //{
    //    // if combat still in progress
    //    if (CombatStatus == CombatStatus.InProgress)
    //    {


    //        if (!TurnEnds)
    //        {
    //            Debug.Log("Select an option: 1. Attack 2. Item 3. Run");
    //            // Get the user input
    //            switch (GameManager.GetSelectedIntValue())
    //            {
    //                case 1:
    //                    Debug.Log("You Selected to attack");
    //                    break;
    //                case 2:
    //                    Debug.Log("You Selected to use Item");
    //                    break;
    //                case 3:
    //                    Debug.Log("You Selected to run");
    //                    CombatResult = CombatResult.AttackerWon;
    //                    break;
    //            }

    //        }


    //    }

    //}

    // Switch turns between attacker and defender
    public void SwitchTurn()
    {
        switch (CombatTurn)
        {
            case CombatTurn.Attacker:
                CombatTurn = CombatTurn.Defender;
                break;
            case CombatTurn.Defender:
                CombatTurn = CombatTurn.Attacker;
                break;
        }
    }

    public void StartTurn()
    {
        // if winner determined, return
        if (checkWinner())
        {
            CombatStatus = CombatStatus.End;
            return;
        }
    }

    public void EndTurn()
    {

    }

    private bool checkWinner()
    {
        if (Attacker.CombatantInfo.HealthPoint < 0)
        {
            CombatResult = CombatResult.AttackerLost;
            return true;
        }
        if (Defender.CombatantInfo.HealthPoint < 0)
        {
            CombatResult = CombatResult.AttackerWon;
            return true;
        }
        return false;
    }

    private void initCombat()
    {

    }

    // Start the combat
    // Attacker is the one who initial the attack which starts the first turn (Ex: This allow the enemy to attack the player first)
    public void StartCombat(ICombatant attacker, ICombatant defender)
    {
        CombatStatus = CombatStatus.Start;
        CombatTurn = CombatTurn.Attacker;
        Attacker = attacker;
        Defender = defender;
        initCombat();
        CombatStatus = CombatStatus.InProgress;
        CurrentCombatant = Attacker;

        Debug.Log(Attacker.Name + " initial the attack!");
        Debug.Log(Defender.Name + " is being attacked!");

        StartTurn();
    }
}
