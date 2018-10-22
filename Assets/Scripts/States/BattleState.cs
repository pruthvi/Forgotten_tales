using System;
using UnityEngine;

public enum BattleTurn { Player, Mob }
public enum PlayerSelectingStatus { NoSelection, Combat }
public class BattleState : GameState
{
    private Battle _battle;
    private CombatManager _combatManager;

    private BattleTurn BattleTurn;

    private Enemy _enemy;
    public Player Player;

    public AudioClip[] ClipPreCombatOptions;
    public AudioClip[] ClipCombatAttackOptions;

    public string[] PreCombatOptions;
    public string[] CombatAttackOptions;

    private float maxPlayerTurnTime = 15;

    private float timer;

    private float mobTimer = 5;

    private int currentHP = 0;

    public PlayerSelectingStatus PlayerSelectingStatus;

    private bool timeOut
    {
        get
        {
            return timer >= maxPlayerTurnTime;
        }
    }

    private bool playerSelecting;

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.Battle;
        }
    }

    public override void OnGUIChange()
    {
        switch (BattleTurn)
        {
            case BattleTurn.Player:
                _gm.UIManager.Header = "Player - HP: " + Player.HP + "/" + Player.MaxHP + " Time Left: " + (maxPlayerTurnTime - (int)timer);
                break;
            case BattleTurn.Mob:
                _gm.UIManager.Header = "Mob - HP: " + _battle.Enemies[0].HP + "/" + _battle.Enemies[0].MaxHP + " Time Left: " + (mobTimer - (int)timer);
                break;
        }
        
    }

    public override void OnEnter()
    {
        _battle = (Battle)_gm.InGameState.CurrentGameEvent;
        _gm.UIManager.Header = "";
        Player.HP = Player.MaxHP;
        _battle.Enemies[0].HP = _battle.Enemies[0].MaxHP;
        if (_battle.Initiator.CombatantType == CombatantType.Mob)
        {
            BattleTurn = BattleTurn.Mob;
        }
        else
        {
            BattleTurn = BattleTurn.Player;
        }
        _gm.UIManager.Content = "";
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (timer + Time.deltaTime < maxPlayerTurnTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            switchTurn();
        }
        OnGUIChange();
        switch (BattleTurn)
        {
            case BattleTurn.Mob:
                if (_battle.Enemies[0].HP <= 0)
                {
                    _gm.ChangeState(GameStateType.InGame);
                }
                if (timer >= mobTimer / 2)
                {
                    //_battle.InitiateFight(_battle.Enemies[0], _gm.Player, _battle.Enemies[0].Spells[0]);
                    //switchTurn();
                }
                break;
            case BattleTurn.Player:
                break;
        }
    }

    private void switchTurn()
    {
        if (BattleTurn == BattleTurn.Player)
        {
            //_gm.Narrator.Play(_gm.AudioBattles[6]);
            //BattleTurn = BattleTurn.Mob;
            //_gm.UIManager.Content = "";
        }
        else
        {
            BattleTurn = BattleTurn.Player;
            onPlayerTurnEnter();
            UpdatePlayerTurnGUI();
        }
        timer = 0;
    }

    private void updateMobTurn()
    {
    //    _enemy.Attack(_player);
    }

    public void UpdatePlayerTurnGUI()
    {
        //if (PlayerSelectingStatus == PlayerSelectingStatus.NoSelection)
        //{
        //    string preCombatMenu = "";
        //    for (int i = 0; i < _gm.PreCombatOptions.Length; i++)
        //    {
        //        // Put > infront of the MenuItem if it was the SelectedIndex
        //        preCombatMenu += (_gm.InputManager.SelectedItemIndex == i ? "> " : "\t") + _gm.PreCombatOptions[i] + "\n";
        //    }
        //    _gm.TextDescription.text = preCombatMenu;
        //}
        //else
        //{
        //    string combatMenu = "";
        //    for (int i = 0; i < _gm.CombatAttackOptions.Length; i++)
        //    {
        //        // Put > infront of the MenuItem if it was the SelectedIndex
        //        combatMenu += (_gm.InputManager.SelectedItemIndex == i ? "> " : "\t") + _gm.CombatAttackOptions[i] + "\n";
        //    }
        //    _gm.TextDescription.text = combatMenu;
        //}
        //switch (_player.Choice)
        //{
        //    case PlayerChoice.Idle:
                
        //        break;
        //    case PlayerChoice.Fight:
                
        //        break;
        //    case PlayerChoice.Attack:
        //        break;
        //    case PlayerChoice.Defense:
        //        break;
        //    case PlayerChoice.Item:
        //        break;
        //}
    }

    private void onPlayerTurnEnter()
    {
        //// Play your turn
        //_gm.Narrator.Play(_gm.AudioBattles[5]);
        //_gm.InputManager.ChangeInputLayer(InputLayer.ChoosePreCombatOption, PreCombatOptions);
        //PlayerSelectingStatus = PlayerSelectingStatus.NoSelection;
        //UpdatePlayerTurnGUI();
        //// If Low Health
        //if (_gm.Player.HP <= _gm.Player.MaxHP / 2)
        //{
        //    _gm.AudioManager.PlaySFX(_gm.AudioBattles[4]);
        //}
        //if (_gm.ClipPreCombatOptions[_gm.InputManager.SelectedItemIndex] != null)
        //{
        //    _gm.Narrator.Play(_gm.ClipPreCombatOptions[_gm.InputManager.SelectedItemIndex]);
        //}
    }

    public void SelectPreCombatOption(int index)
    {
        //if (index == 0)
        //{
        //    // Fight
        //    _player.Choice = PlayerChoice.Fight;
        //    _gm.InputManager.ChangeInputLayer(InputLayer.ChooseCombatOption, _gm.PreCombatOptions.Length);
        //    PlayerSelectingStatus = PlayerSelectingStatus.Combat;
        //    UpdatePlayerTurnGUI();
        //}
        //else if (index == 1)
        //{
        //    // Item
        //    _player.Choice = PlayerChoice.Item;
        //    _gm.InputManager.ChangeInputLayer(InputLayer.ChooseItemOption, _player.InventoryManager.Items.Count);
        //}
        //else if (index == 2)
        //{
        //    _battle.BattleResult = BattleResult.Lose;
        //    _gm.ChangeState(GameStateType.InGame);
        //}

    }

    public void SelectCombatOption(int index)
    {
        //if (index == 0)
        //{
        //    // Spell
        //    _battle.InitiateFight(_player, _battle.Enemies[0], _player.Spells[0]);
        //}
        //else if (index == 1)
        //{
        //    // Defense
        //    _player.Defense = true;
        //}
        //_gm.InputManager.ChangeInputLayer(InputLayer.ChoosePreCombatOption, _gm.PreCombatOptions.Length);
        //_gm.TextDescription.text = "";
        //switchTurn();
    }

    public override void OnInput()
    {
        
    }
}