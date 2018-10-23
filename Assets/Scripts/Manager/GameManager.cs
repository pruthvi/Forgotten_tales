using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Managers
    [HideInInspector]
    public AudioManager AudioManager;
    [HideInInspector]
    public SettingManager SettingManager;
    [HideInInspector]
    public InputManager InputManager;
    [HideInInspector]
    public UIManager UIManager;
    [HideInInspector]
    public Narrator Narrator;

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

    public InGameState InGameState
    {
        get
        {
            return (InGameState)States[5];
        }
    }

    private GameStateType _currentGameStateType;
    public GameStateType CurrentGameStateType
    {
        get
        {
            return _currentGameStateType;
        }
    }

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

        AudioManager = GetComponentInChildren<AudioManager>();
        SettingManager = GetComponentInChildren<SettingManager>();
        InputManager = GetComponentInChildren<InputManager>();
        UIManager = GetComponentInChildren<UIManager>();
        Narrator = GetComponentInChildren<Narrator>();
    }

    void Start()
    {
        States.Add(GetComponent<SplashScreenState>());
        States.Add(GetComponent<MainMenuState>());
        States.Add(GetComponent<ControlsState>());
        States.Add(GetComponent<SettingState>());
        States.Add(GetComponent<PreGameState>());
        States.Add(GetComponent<InGameState>());


        foreach (GameState gs in States)
        {
            gs.InitState();
        }

        ChangeState(GameStateType.SplashScreen);
    }



    // Call OnUpdate when MonoBehaviour call Update
    void Update()
    {
        // Update only if GameState has the same type
        if (_currentGameState.GameStateType == _currentGameStateType)
        {
            _currentGameState.OnInput();
            _currentGameState.OnUpdate();
        }
    }

    public void ChangeState(GameStateType stateType)
    {
        if (_currentGameState != null)
        {
            Narrator.Stop();
            _currentGameState.OnExit();
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
            case GameStateType.Setting:
                _currentGameState = States[3];
                break;
            case GameStateType.PreGame:
                _currentGameState = States[4];
                break;
            case GameStateType.InGame:
                _currentGameState = States[5];
                break;
            case GameStateType.Battle:
                _currentGameState = States[6];
                break;
        }
        Narrator.Stop();
        _currentGameStateType = stateType;
        _currentGameState.OnEnter();
    }

    public void NextEvent()
    {
        if (_currentGameState.GameStateType == GameStateType.InGame)
        {
            // Get the InGameState
            InGameState.NextEvent();
        }
    }
}
