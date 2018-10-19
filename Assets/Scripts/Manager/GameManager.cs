using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameProgress { Prologue, Intro, DialogueBegin, EndOfAct }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Managers
    public AudioManager AudioManager { get; private set; }
    public SettingManager SettingManager { get; private set; }
    public InputManager InputManager { get; private set; }
    public CombatManager CombatManager { get; private set; }
    public Narrator Narrator;

    // GUI
    public Text TextUI;
    public Text TextDescription;

    // Splash Screen
    public bool SkipSplashScreen;
    public AudioClip[] SplashScreenAudio;
    public string[] SplashScreenText;

    // Main Menu
    public AudioClip[] ClipMainMenuGuide;
    public AudioClip[] ClipMenuItem;

    // Narrator
    public Act[] Acts;

    // Game Related

    private List<GameState> _states = new List<GameState>();
    public List<GameState> States
    {
        get
        {
            return _states;
        }
    }

    private GameState _currentGameState;
    public GameState CurrentGameState
    {
        get
        {
            return _currentGameState;
        }
    }

    public GameProgress GameProgress;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        initGame();
    }

    public void initGame()
    {
        AudioManager = GetComponentInChildren<AudioManager>();
        SettingManager = new SettingManager(this);
        InputManager = new InputManager(this);

        Narrator = new Narrator(this, AudioManager.NarrativeSource, Acts);

        Narrator.Begin(0);

        States.Add(new SplashScreenState(this));
        States.Add(new MainMenuState(this));
        States.Add(new ControlsState(this));
        States.Add(new SettingsState(this));
        States.Add(new PreGameState(this));
        States.Add(new InGameState(this));

        ChangeState(GameStateType.SplashScreen);
    }

    void Update()
    {
        InputManager.Update();
        Narrator.UpdateStatus();
        _currentGameState.OnStateUpdate();
    }

    public void ChangeState(GameStateType stateType)
    {
        if (_currentGameState != null)
        {
            Narrator.Stop();
            _currentGameState.OnStateExit();
        }
        switch (stateType)
        {
            case GameStateType.SplashScreen:
                _currentGameState = States[0];
                break;
            case GameStateType.MainMenu:
                _currentGameState = States[1];
                break;
            case GameStateType.Controls:
                _currentGameState = States[2];
                break;
            case GameStateType.Settings:
                _currentGameState = States[3];
                break;
            case GameStateType.PreGame:
                _currentGameState = States[4];
                break;
            case GameStateType.InGame:
                _currentGameState = States[5];
                break;
        }
        Narrator.Stop();
        _currentGameState.OnStateEnter();
    }
}
