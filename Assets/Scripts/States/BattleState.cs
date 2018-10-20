using UnityEngine;

public enum BattleTurn { Player, Mob }
public enum PlayerSelectingStatus { NoSelection, Combat }
public class BattleState : GameState
{
    private Battle _battle;
    private CombatManager _combatManager;

    private BattleTurn BattleTurn;

    private Enemy _enemy;
    private Player _player;

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

    public BattleState(GameManager gm) : base(gm)
    {
        _gameManager = gm;
    }

    public override GameStateType GameStateType
    {
        get
        {
            return GameStateType.Battle;
        }
    }

    public override void UpdateGUI()
    {
        switch (BattleTurn)
        {
            case BattleTurn.Player:
                _gameManager.TextUI.text = "Player - HP: " + _gameManager.Player.HP + "/" + _gameManager.Player.MaxHP + " Time Left: " + (maxPlayerTurnTime - (int)timer);
                break;
            case BattleTurn.Mob:
                _gameManager.TextUI.text = "Mob - HP: " + _battle.Enemies[0].HP + "/" + _battle.Enemies[0].MaxHP + " Time Left: " + (mobTimer - (int)timer);
                break;
        }
        
    }

    public override void OnStateEnter()
    {
        //_gameManager.InputManager.
        _battle = (Battle)_gameManager.InGameState.CurrentGameEvent;
        _gameManager.TextUI.text = "";
        _player = _gameManager.Player;
        _player.HP = _player.MaxHP;
        _battle.Enemies[0].HP = _battle.Enemies[0].MaxHP;
        if (_battle.Initiator.CombatantType == CombatantType.Mob)
        {
            BattleTurn = BattleTurn.Mob;
        }
        else
        {
            BattleTurn = BattleTurn.Player;
        }
        _gameManager.TextDescription.text = "";
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        if (timer + Time.deltaTime < maxPlayerTurnTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            switchTurn();
        }
        UpdateGUI();
        switch (BattleTurn)
        {
            case BattleTurn.Mob:
                if (_battle.Enemies[0].HP <= 0)
                {
                    _gameManager.ChangeState(GameStateType.InGame);
                }
                if (timer >= mobTimer / 2)
                {
                    _battle.InitiateFight(_battle.Enemies[0], _gameManager.Player, _battle.Enemies[0].Spells[0]);
                    switchTurn();
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
            _gameManager.Narrator.Play(_gameManager.AudioBattles[6]);
            BattleTurn = BattleTurn.Mob;
            _gameManager.TextDescription.text = "";
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
        if (PlayerSelectingStatus == PlayerSelectingStatus.NoSelection)
        {
            string preCombatMenu = "";
            for (int i = 0; i < _gameManager.PreCombatOptions.Length; i++)
            {
                // Put > infront of the MenuItem if it was the SelectedIndex
                preCombatMenu += (_gameManager.InputManager.SelectedItemIndex == i ? "> " : "\t") + _gameManager.PreCombatOptions[i] + "\n";
            }
            _gameManager.TextDescription.text = preCombatMenu;
        }
        else
        {
            string combatMenu = "";
            for (int i = 0; i < _gameManager.CombatAttackOptions.Length; i++)
            {
                // Put > infront of the MenuItem if it was the SelectedIndex
                combatMenu += (_gameManager.InputManager.SelectedItemIndex == i ? "> " : "\t") + _gameManager.CombatAttackOptions[i] + "\n";
            }
            _gameManager.TextDescription.text = combatMenu;
        }
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
        // Play your turn
        _gameManager.Narrator.Play(_gameManager.AudioBattles[5]);
        _gameManager.InputManager.ChangeInputLayer(InputLayer.ChoosePreCombatOption, _gameManager.PreCombatOptions.Length);
        PlayerSelectingStatus = PlayerSelectingStatus.NoSelection;
        UpdatePlayerTurnGUI();
        // If Low Health
        if (_gameManager.Player.HP <= _gameManager.Player.MaxHP / 2)
        {
            _gameManager.AudioManager.PlaySFX(_gameManager.AudioBattles[4]);
        }
        if (_gameManager.ClipPreCombatOptions[_gameManager.InputManager.SelectedItemIndex] != null)
        {
            _gameManager.Narrator.Play(_gameManager.ClipPreCombatOptions[_gameManager.InputManager.SelectedItemIndex]);
        }
    }

    public void SelectPreCombatOption(int index)
    {
        if (index == 0)
        {
            // Fight
            _player.Choice = PlayerChoice.Fight;
            _gameManager.InputManager.ChangeInputLayer(InputLayer.ChooseCombatOption, _gameManager.PreCombatOptions.Length);
            PlayerSelectingStatus = PlayerSelectingStatus.Combat;
            UpdatePlayerTurnGUI();
        }
        else if (index == 1)
        {
            // Item
            _player.Choice = PlayerChoice.Item;
            _gameManager.InputManager.ChangeInputLayer(InputLayer.ChooseItemOption, _player.InventoryManager.Items.Count);
        }
        else if (index == 2)
        {
            _battle.BattleResult = BattleResult.Lose;
            _gameManager.ChangeState(GameStateType.InGame);
        }

    }

    public void SelectCombatOption(int index)
    {
        if (index == 0)
        {
            // Spell
            _battle.InitiateFight(_player, _battle.Enemies[0], _player.Spells[0]);
        }
        else if (index == 1)
        {
            // Defense
            _player.Defense = true;
        }
        _gameManager.InputManager.ChangeInputLayer(InputLayer.ChoosePreCombatOption, _gameManager.PreCombatOptions.Length);
        _gameManager.TextDescription.text = "";
        switchTurn();
    }
}