using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Unity;

public enum BattleTurn { Player, Mob }
public enum BattleResult { None, PlayerWin, PlayerLose }
public enum PlayerChoice { NoAction, Fight, Item, Run, Spell, Defense }
public enum PlayerChoiceState { None, PreCombat, FightEnemy, UseItem }
[CreateAssetMenu(menuName = "Forgotten Tale/Game Event/Battle")]
public class Battle : GameEvent
{
    private BattleTurn BattleTurn;

    private Player _player;

    public AudioClip SFXTicking;
    public AudioClip[] ClipPreCombatOptions;
    public AudioClip[] ClipCombatAttackOptions;

    public string[] PreCombatOptions;
    public string[] CombatAttackOptions;

    public bool InitiatedByEnemy;
    public Combatant Enemy;
    [HideInInspector]
    public BattleResult BattleResult;

    public int MaxPlayerTimePerTurn = 15;
    public int MaxMobTimePerTurn = 5;

    private float _timer = 0;

    private float _mobAttackTimer = 0;

    private Combatant _currentCombant;

    private PlayerChoice _playerChoice;
    private PlayerChoiceState _playerChoiceState;

    private bool _playTicking;

    public override GameEventType GameEventType
    {
        get
        {
            return GameEventType.Battle;
        }
    }

    public GameEvent WinEvent;
    public GameEvent LoseEvent;

    public void InitiateFight(Combatant attacker, Combatant defender, Spell spell)
    {
        attacker.Attack(defender, spell);
    }

    public override void OnEnter()
    {
        _playerChoiceState = PlayerChoiceState.None;

        _gm = GameManager.Instance;

        _gm.UIManager.TextContent.alignment = TextAnchor.LowerLeft;
        _gm.UIManager.Header = "";
        _gm.UIManager.Content = "";

        Init();
        
        OnGUIChange();
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (hasWinner())
        {
            return;
        }
        switch (BattleTurn)
        {
            case BattleTurn.Player:
                if (MaxPlayerTimePerTurn - _timer <= MaxPlayerTimePerTurn /3 && _playTicking)
                {
                    _gm.AudioManager.Play(SFXTicking, AudioChannel.SFX2);
                    _playTicking = false;
                }
                if (_timer + Time.deltaTime < MaxPlayerTimePerTurn)
                {
                    _timer += Time.deltaTime;
                }
                else
                {
                    switchTurn();
                }
                break;
            case BattleTurn.Mob:
                if (_timer + Time.deltaTime < MaxMobTimePerTurn)
                {
                    _timer += Time.deltaTime;
                }
                if (_timer >= _mobAttackTimer)
                {
                    InitiateFight(Enemy, _player, Enemy.Spells[Random.Range(0, Enemy.Spells.Length)]);
                    switchTurn();
                }
                break;
        }
        OnGUIChange();
    }

    public override void OnGUIChange()
    {
        updateCombatantInfo();
        switch (_playerChoiceState)
        {
            case PlayerChoiceState.PreCombat:
                string preCombatOptions = "";
                for (int i = 0; i < PreCombatOptions.Length; i++)
                {
                    preCombatOptions += (_gm.InputManager.SelectedItemIndex == i ? "> " : "\t") + PreCombatOptions[i] + "\n";
                }
                _gm.UIManager.Content = preCombatOptions;
                break;
            case PlayerChoiceState.FightEnemy:
                string combatAttackOptions = "";
                for (int i = 0; i < CombatAttackOptions.Length; i++)
                {
                    combatAttackOptions += (_gm.InputManager.SelectedItemIndex == i ? "> " : "\t") + CombatAttackOptions[i] + "\n";
                }
                _gm.UIManager.Content = combatAttackOptions;
                break;
            case PlayerChoiceState.UseItem:
                string items = "";
                for (int i = 0; i < CombatAttackOptions.Length; i++)
                {
                    items += (_gm.InputManager.SelectedItemIndex == i ? "> " : "\t") + _player.InventoryManager.Items[_gm.InputManager.SelectedItemIndex].name + "\n";
                }
                _gm.UIManager.Content = items;
                break;
        }
    }

    private void updateCombatantInfo()
    {
        string timeLeft = BattleTurn == BattleTurn.Player ? (int)(MaxPlayerTimePerTurn - _timer) + "" : (int)(_mobAttackTimer - _timer) + "";
        _gm.UIManager.Header = _currentCombant.Name + "'s Turn - HP: " + _currentCombant.HP + "/" + _currentCombant.MaxHP + " | MP: " + _currentCombant.MP + "/" + _currentCombant.MaxMP + " | Time Left: " + timeLeft;
    }

    private void switchTurn()
    {
        _gm.UIManager.Content = "";
        if (BattleTurn == BattleTurn.Player)
        {
            _currentCombant = Enemy;
            BattleTurn = BattleTurn.Mob;
            _mobAttackTimer = Random.Range(1, MaxMobTimePerTurn);
            _playerChoiceState = PlayerChoiceState.None;
            _gm.UIManager.Content = "";
        }
        else
        {
            _currentCombant = _player;
            BattleTurn = BattleTurn.Player;
            _playerChoiceState = PlayerChoiceState.PreCombat;
            _gm.InputManager.SetMenuItemLimit(PreCombatOptions.Length);
            _playTicking = true;
        }

        _timer = 0;
    }

    public override void OnInput()
    {
        if (BattleTurn == BattleTurn.Player)
        {
            switch (_playerChoiceState)
            {
                case PlayerChoiceState.PreCombat:
                    if (_gm.InputManager.SelectionUp())
                    {
                        _gm.Narrator.Play(ClipPreCombatOptions[_gm.InputManager.SelectedItemIndex], PlayType.Dialogue);
                    }
                    if (_gm.InputManager.SelectionDown())
                    {
                        _gm.Narrator.Play(ClipPreCombatOptions[_gm.InputManager.SelectedItemIndex], PlayType.Dialogue);
                    }
                    if (_gm.InputManager.SelectionConfirm())
                    {
                        if (_gm.InputManager.SelectedItemIndex == 0)
                        {
                            _playerChoiceState = PlayerChoiceState.FightEnemy;
                            _gm.InputManager.SetMenuItemLimit(CombatAttackOptions.Length);
                        }
                        else if (_gm.InputManager.SelectedItemIndex == 1)
                        {
                            _playerChoiceState = PlayerChoiceState.UseItem;
                        }
                        else if (_gm.InputManager.SelectedItemIndex == 2)
                        {
                            BattleResult = BattleResult.PlayerLose;
                        }
                    }
                    break;
                case PlayerChoiceState.FightEnemy:
                    if (_gm.InputManager.SelectionUp())
                    {
                        _gm.Narrator.Play(ClipPreCombatOptions[_gm.InputManager.SelectedItemIndex], PlayType.Dialogue);
                    }
                    if (_gm.InputManager.SelectionDown())
                    {
                        _gm.Narrator.Play(ClipPreCombatOptions[_gm.InputManager.SelectedItemIndex], PlayType.Dialogue);
                    }
                    if (_gm.InputManager.SelectionConfirm())
                    {
                        if (_gm.InputManager.SelectedItemIndex == 0)
                        {
                            InitiateFight(_player, Enemy, _player.Spells[0]);
                        }
                        else if (_gm.InputManager.SelectedItemIndex == 1)
                        {
                            _player.Defense = true;
                        }
                        switchTurn();
                    }
                    break;
                case PlayerChoiceState.UseItem:

                    break;
            }
        }
    }

    public override void Init()
    {
        BattleResult = BattleResult.None;
        _player = _gm.InGameState.Player;
        _player.HP = _player.MaxHP;
        _player.MP = _player.MaxMP;

        Enemy.HP = Enemy.MaxHP;
        Enemy.MP = Enemy.MaxMP;


        if (InitiatedByEnemy)
        {
            _currentCombant = Enemy;
            BattleTurn = BattleTurn.Mob;
            _mobAttackTimer = Random.Range(0, MaxMobTimePerTurn);
        }
        else
        {
            _currentCombant = _player;
            BattleTurn = BattleTurn.Player;
        }
    }

    private bool hasWinner()
    {
        switch (BattleTurn)
        {
            case BattleTurn.Mob:
                if (Enemy.HP <= 0)
                {
                    BattleResult = BattleResult.PlayerWin;
                    return true;
                }
                break;
            case BattleTurn.Player:
                if (_player.HP <= 0)
                {
                    BattleResult = BattleResult.PlayerLose;
                    return true;
                }
                break;
        }
        return false;
    }
}