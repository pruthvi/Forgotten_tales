using UnityEngine;

public enum BattleTurn { Player, Mob}
public class BattleState : GameState
{
    private Battle _battle;
    private CombatManager _combatManager;

    private string[] choices = { "Attack", "Use Item", "Run" };

    private BattleTurn BattleTurn;

    private Enemy _enemy;
    private Player _player;

    public BattleState(GameManager gm) : base(gm)
    {

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
        string turner = "";
        switch (BattleTurn)
        {
            case BattleTurn.Player:
                turner = "Player";
                break;
            case BattleTurn.Mob:
                turner = "Enemy";
                break;
        }
        _gameManager.TextUI.text = turner;
    }

    public override void OnStateEnter()
    {
        _combatManager = _gameManager.CombatManager;
        _battle = _combatManager.CurrentBattle;

        if (_combatManager.Attacker.CombatantType == CombatantType.Mob)
        {
            BattleTurn = BattleTurn.Mob;
        }
        else
        {
            BattleTurn = BattleTurn.Player;
        }
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        switch (BattleTurn)
        {
            case BattleTurn.Mob:
                updateMobTurn();
                break;
            case BattleTurn.Player:
                onPlayerTurnEnter();
                updatePlayerTurn();
                break;
        }
    }

    private void updateMobTurn()
    {
    //    _enemy.Attack(_player);
    }

    private void updatePlayerTurn()
    {
        switch (_player.Choice)
        {
            case PlayerChoice.Idle:
                // Check for timeout
                break;
            case PlayerChoice.Attack:
                _gameManager.InputManager.ChangeInputLayer(InputLayer.ChooseCombatOption, choices.Length);
                break;
            case PlayerChoice.Item:

                _gameManager.InputManager.ChangeInputLayer(InputLayer.ChooseItemOption, choices.Length);
                break;
            case PlayerChoice.Run:
                // End battle
                _battle.BattleResult = BattleResult.Lose;
           //     _battle.End();
                break;
        }
    }

    private void onPlayerTurnEnter()
    {
        _gameManager.InputManager.ChangeInputLayer(InputLayer.ChoosePreCombatOption, choices.Length);
    }
}