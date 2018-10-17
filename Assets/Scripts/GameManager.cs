using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputLayer { MainMenu, DialogueChoice, CombatChoice, Combat }
public enum GameState { MainMenu, InGame, Settings }
public enum NarrativeType { NarrativePrologue, NarrativeDialogue, NarrativeCombat, NarrativeCombatChoice }
public enum NarratorStatus { Idle, Speaking, Completed }
public enum NarratorProgress { Prologue, Intro, Dialogue, DialogueOption }
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private AudioManager audioManager;
    private CombatManager combatManager;
    public Player Player { get; private set; }

    public int MinSelectionInput = 1;
    public int MaxSelectionInput = 3;

    public InputLayer InputLayer;
    private GameState _gameState;
    public GameState GameState
    {
        get
        {
            return _gameState;
        }
        set
        {
            _gameState = value;
            switch (value)
            {
                case GameState.MainMenu:
                    updateMainMenuDisplay();
                    break;
                case GameState.Settings:
                    updateSettingsDisplay();
                    break;
            }
        }
    }

    // Narrative
    public NarrativeType NarrativeType;
    public NarratorProgress NarratorProgress;
    public NarratorStatus NarratorStatus;

    // Input
    public InputStatus InputStatus;

    [SerializeField]
    public int LastSelectionInput { get; private set; }



    public Text textUI;
    public Text textDescription;

    public Act[] Act;

    private Act _currentAct;

    private GameEvent _currentEvent;

    private int _currentActIndex = 0;
    private int _currentDialogueIndex = 0;

    private AudioSource _backgroundSource;
    private AudioSource _sfxSource;
    private AudioSource _narrativeSource;

    private string[] _menuItems = { "Start", "Settings", "Exit" };
    private int _menuSelectedIndex = 0;
    private int _optionSelectedIndex = 0;

    private int _introIndex = 0;

    private string textInstruction = "[R] - Replay | [F] Fastforward | [Esc] Skip | ";

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

        audioManager = GetComponentInChildren<AudioManager>();

        _backgroundSource = audioManager.BackgroundSource;
        _sfxSource = audioManager.SfxSource;
        _narrativeSource = audioManager.NarrativeSource;
    }

    void InitGame()
    {
        Player = new Player("Main Character", 100, 50);
        Debug.Log("Player - " + Player.Name);

        combatManager = new CombatManager();
        LastSelectionInput = -1;

        NarratorProgress = NarratorProgress.Prologue;
        NarratorStatus = NarratorStatus.Idle;

        InputStatus = InputStatus.Idle;

        _currentAct = Act[0];

        updateMainMenuDisplay();
        //GameState = GameState.Prologue;
        //InputLayer = InputLayer.MainMenu;
    }

    // Use this for initialization
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Check which state is the game in EX) MainMenu, InGame, Settings
        switch (GameState)
        {
            case GameState.MainMenu:
                updateSelection();
                break;
            case GameState.InGame:
                // Update Narrator Progress
                updateNarratorProgress();
                updateInGame();
                updateSelection();
                // update narrative type

                //narrativeTypeUpdate();

                // update input layer

                //inputLayerUpdate();

                // update game state

                //gameStateUpdate();
                break;
            case GameState.Settings:
                updateSettingsDisplay();
                updateSettings();
                break;
        }
    }

    private void updateMainMenuDisplay()
    {
        string menu = "";
        for (int i = 0; i < _menuItems.Length; i++)
        {
            menu += (_menuSelectedIndex == i ? "> " : "\t") + _menuItems[i] + "\n";
        }
        textDescription.text = menu;
    }

    private void updateSelection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (GameState == GameState.MainMenu)
            {
                if (_menuSelectedIndex - 1 < 0)
                {
                    _menuSelectedIndex = _menuItems.Length - 1;
                }
                else
                {
                    _menuSelectedIndex--;
                }
                updateMainMenuDisplay();
            }
            else if (GameState == GameState.InGame)
            {
                if (_optionSelectedIndex - 1 < 0)
                {
                    _optionSelectedIndex = _currentAct.CurrentDialogue.Options.Count - 1;
                }
                else
                {
                    _optionSelectedIndex--;
                    NarratorStatus = NarratorStatus.Idle;
                }
                updateOptionDisplay();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (GameState == GameState.MainMenu)
            {
                if (_menuSelectedIndex + 1 >= _menuItems.Length)
                {
                    _menuSelectedIndex = 0;
                }
                else
                {
                    _menuSelectedIndex++;
                }
                updateMainMenuDisplay();
            }
            else if (GameState == GameState.InGame)
            {
                if (_optionSelectedIndex + 1 >= _currentAct.CurrentDialogue.Options.Count)
                {
                    _optionSelectedIndex = 0;
                }
                else
                {
                    _optionSelectedIndex++;
                }
                updateOptionDisplay();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameState == GameState.MainMenu)
            {
                switch (_menuSelectedIndex)
                {
                    case 0:
                        // Start Game
                        GameState = GameState.InGame;
                        updateInGameUIDisplay();
                        break;
                    case 1:
                        // Go to Setting
                        GameState = GameState.Settings;
                        break;
                    case 2:
                        // Exit Game
                        Debug.Log("Exit Game");
                        break;
                }
            }
            else if(GameState == GameState.InGame)
            {
                // Move to next dialogue/event
                if (InputStatus == InputStatus.Selecting)
                {
                    InputStatus = InputStatus.Completed;
                }
            }
            
        }
    }

    private void updateSettingsDisplay()
    {
        textUI.text = "Settings\n[Esc] Back to Menu";
    }

    private void updateSettings()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameState = GameState.MainMenu;
        }
    }

    private void updateNarratorProgress()
    {
        switch (NarratorProgress)
        {
            case NarratorProgress.Prologue:
                // if speaking
                // if idel say the text
                if (NarratorStatus == NarratorStatus.Idle)
                {
                    textDescription.text = _currentAct.PrologueTextDescription;
                    _narrativeSource.clip = _currentAct.AudioPrologue;
                    _narrativeSource.Play();
                    NarratorStatus = NarratorStatus.Speaking;
                    return;
                }
                // check if narrator speaking
                checkIfFinishedPlay();
                // move to next stage(intro) if narrator completed speaking
                if (NarratorStatus == NarratorStatus.Completed)
                {
                    NarratorProgress = NarratorProgress.Intro;
                    NarratorStatus = NarratorStatus.Idle;
                }
                break;
            case NarratorProgress.Intro:
                if (NarratorStatus == NarratorStatus.Idle)
                {
                    textDescription.text = _currentAct.IntroTextDescriptions[_introIndex];
                    _narrativeSource.clip = _currentAct.AudioIntros[_introIndex];
                    _narrativeSource.Play();
                    NarratorStatus = NarratorStatus.Speaking;
                }
                checkIfFinishedPlay();
                if (NarratorStatus == NarratorStatus.Completed)
                {
                    _introIndex++;
                    NarratorStatus = NarratorStatus.Idle;
                    if (_introIndex == _currentAct.IntroTextDescriptions.Length)
                    {
                        NarratorProgress = NarratorProgress.Dialogue;
                    }
                }
                break;
            case NarratorProgress.Dialogue:
                if (NarratorStatus == NarratorStatus.Idle)
                {
                    textDescription.text = _currentAct.CurrentDialogue.TextDescription;
                    _narrativeSource.clip = _currentAct.CurrentDialogue.AudioDescription;
                    _narrativeSource.Play();
                    NarratorStatus = NarratorStatus.Speaking;
                }
                checkIfFinishedPlay();
                if (NarratorStatus == NarratorStatus.Completed)
                {
                    NarratorProgress = NarratorProgress.DialogueOption;
                    NarratorStatus = NarratorStatus.Idle;
                    _optionSelectedIndex = 0;
                    InputStatus = InputStatus.Idle;
                }
                break;
            case NarratorProgress.DialogueOption:
                if (NarratorStatus == NarratorStatus.Idle)
                {
                  //  _narrativeSource.clip = _currentAct.CurrentDialogue.Options[_optionSelectedIndex].AudioDescription;
                    updateOptionDisplay();
                    InputStatus = InputStatus.Selecting;
                }
                checkIfFinishedPlay();
                if (NarratorStatus == NarratorStatus.Completed)
                {
                    if (_optionSelectedIndex == _currentAct.CurrentDialogue.Options.Count && _optionSelectedIndex != 0)
                    {
                        NarratorProgress = NarratorProgress.Dialogue;
                    }
                }
                break;
        }
    }

    private void updateOptionDisplay()
    {
        string options = "";
        for (int i = 0; i < _currentAct.CurrentDialogue.Options.Count; i++)
        {
            options += (_optionSelectedIndex == i ? "> " : "\t") + " Option " + (i + 1) + ": \n\t\t" + _currentAct.CurrentDialogue.Options[i].TextDescription + "\n";
        }
        textDescription.text = _currentAct.CurrentDialogue.TextDescription + "\n[" + (_optionSelectedIndex + 1) + "]" + "\n" + options;
        if (_optionSelectedIndex < _currentAct.CurrentDialogue.Options.Count - 1)
        {
            _narrativeSource.clip = _currentAct.CurrentDialogue.Options[_optionSelectedIndex].AudioDescription;
            NarratorStatus = NarratorStatus.Speaking;
            _narrativeSource.Play();
        }
    }

    private void checkIfFinishedPlay()
    {
        if (NarratorStatus == NarratorStatus.Speaking)
        {
            // if current narrative source not playing set the narrator status to completed
            if (!_narrativeSource.isPlaying)
            {
                NarratorStatus = NarratorStatus.Completed;
            }
        }
    }

    private void updateInGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _narrativeSource.Play();
            NarratorStatus = NarratorStatus.Speaking;
        }
        // If currently playing options
        if (NarratorProgress == NarratorProgress.DialogueOption)
        {
            updateSelection();
        }
        if (NarratorStatus == NarratorStatus.Speaking)
        {
            // Skip
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _narrativeSource.Stop();
                NarratorStatus = NarratorStatus.Completed;
            }

            // Fastforward
            if (Input.GetKeyDown(KeyCode.F))
            {
                audioManager.FastForwardNarrative();
                updateInGameUIDisplay();
            }
        }


        // Update Input
        if (InputStatus == InputStatus.Completed)
        {
            if (_currentAct.NextDialogue(_optionSelectedIndex))
            {

                updateInGameUIDisplay();
                InputStatus = InputStatus.Idle;
            }
        }
    }

    private void updateInGameUIDisplay()
    {
        textUI.text = textInstruction + "Speed: x" + audioManager.FastForwardModifier;
    }

    //private void narratorStatusUpdate()
    //{
    //    switch (NarratorStatus)
    //    {
    //        case NarratorStatus.Idel:
    //            // 
    //            break;
    //        case NarratorStatus.Speaking:
    //            // Skip

    //            // Fastforward
    //            break;
    //        case NarratorStatus.Completed:
    //            break;
    //    }
    //}

    private void narrativeTypeUpdate()
    {
        switch (NarrativeType)
        {
            case NarrativeType.NarrativePrologue:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    audioManager.NarrativeSource.Stop();
                    _currentDialogueIndex++;
                }
                break;
            case NarrativeType.NarrativeDialogue:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    LastSelectionInput = 1;
                    Debug.Log("You choose " + LastSelectionInput);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    LastSelectionInput = 2;
                    Debug.Log("You choose " + LastSelectionInput);
                }
                return;
            case NarrativeType.NarrativeCombat:
                break;
            case NarrativeType.NarrativeCombatChoice:
                break;
        }
    }

    private void inputLayerUpdate()
    {
        //switch (GameState)
        //{
        //    case GameState.Prologue:
        //        displayPrologueTextAndPlay();
        //        break;
        //    case GameState.Dialogue:
        //        displayDialogueTextWithOptionAndPlay();
        //        break;
        //    case GameState.Option:
        //        break;
        //    case GameState.Combat:
        //        if (combatManager.CombatStatus != CombatStatus.InProgress)
        //        {
        //            if (Input.GetKeyDown(KeyCode.B))
        //            {
        //                Enemy goblin = new Enemy("Goblin", 100, 0);

        //                combatManager.StartCombat(goblin, Player);

        //            }
        //        }
        //        break;
        //}
    }

    private void gameStateUpdate()
    {

    }
    private void displayPrologueTextAndPlay()
    {
        _currentAct = Act[_currentActIndex];
        if (_currentAct == null)
        {
            return;
        }

        textDescription.text = _currentAct.PrologueTextDescription + "\n\n Press [Enter] to continue...";
        audioManager.NarrativeSource.clip = _currentAct.AudioPrologue;
        audioManager.NarrativeSource.Play();

        // InputLayer = InputLayer.Narrative;
    }

    /* Switch to next GameEvent
        Dialogue -> Dialogue
        Dialogue -> Combat
    */
    public void Next()
    {
        switch (_currentEvent.EventType)
        {
            case EventType.Dialogue:
                //   Dialogue dialogue = _currentEvent;                           
                break;
            case EventType.Combat:
                // Combat eventCombat = (Combat) _currentEvent;
                // switch(eventCombat.CombatResult)
                // win
                // _currentEvent = eventCombat.WinEvent;
                // lost
                //_currentEvent = eventCombat.LostEvent;
                break;
        }
    }

    private void updateLastInput()
    {

        //switch (InputLayer)
        //{
        //    // This layer only check the up/down keys
        //    case InputLayer.MainMenu:
        //        if (Input.GetKeyUp(KeyCode.UpArrow))
        //        {
        //            LastSelectionInput = LastSelectionInput - 1 <= MinSelectionInput ? MinSelectionInput : LastSelectionInput - 1;
        //        }

        //        if (Input.GetKeyUp(KeyCode.DownArrow))
        //        {
        //            LastSelectionInput = LastSelectionInput + 1 >= MaxSelectionInput ? MaxSelectionInput : LastSelectionInput + 1;
        //        }
        //        break;
        //    // This layer only check the 1/2/3 keys
        //    case InputLayer.CombatChoice:
        //        if (Input.GetKeyUp(KeyCode.Alpha1))
        //        {
        //            LastSelectionInput = 1;
        //        }

        //        if (Input.GetKeyUp(KeyCode.Alpha2))
        //        {
        //            LastSelectionInput = 2;
        //        }

        //        if (Input.GetKeyUp(KeyCode.Alpha3))
        //        {
        //            LastSelectionInput = 3;
        //        }
        //        break;
        //    case InputLayer.Combat:
        //        break;
        //}

    }
}
