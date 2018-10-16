using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputLayer { MainMenu, DialogueChoice, CombatChoice, Combat }
public enum GameState { Prologue, Dialogue, Option, Combat }
public enum NarrativeType { NarrativePrologue, NarrativeDialogue, NarrativeCombat, NarrativeCombatChoice }

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private AudioManager audioManager;
    private CombatManager combatManager;
    public Player Player { get; private set; }

    public int MinSelectionInput = 1;
    public int MaxSelectionInput = 3;

    public InputLayer InputLayer;
    public GameState GameState;
    public NarrativeType NarrativeType;

    [SerializeField]
    public int LastSelectionInput { get; private set; }

   

    public Text text;
   
    public Act[] Act;

    private Act _currentAct;
    private Dialogue _currentDialogue;
    private Option[] _currentOptions;

    private int _currentActIndex = 0;
    private int _currentDialogueIndex = 0;

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

        
    }

    void InitGame()
    {
        Player = new Player("Main Character", 100, 50);
        Debug.Log("Player - " + Player.Name);

        combatManager = new CombatManager();
        LastSelectionInput = -1;

        GameState = GameState.Prologue;
        InputLayer = InputLayer.MainMenu;
    }

    // Use this for initialization
    void Start()
    {
        InitGame();
    }

    public static int GetSelectedIntValue()
    {
        int result = -1;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            result = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            result = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            result = 3;
        }

        if(result != -1)
            Debug.Log("You Selected " + GetSelectedIntValue());

        return result;
    }

    // Update is called once per frame
    void Update()
    {
        // update narrative type

        narrativeTypeUpdate();

        // update input layer

        inputLayerUpdate();

        // update game state

        gameStateUpdate();
    }

    private void narrativeTypeUpdate()
    {
        switch (NarrativeType)
        {
            case NarrativeType.NarrativePrologue:
                if (Input.GetKeyDown(KeyCode.F))
                {
                    GameState = GameState.Dialogue;
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
        switch (GameState)
        {
            case GameState.Prologue:
                displayPrologueTextAndPlay();
                break;
            case GameState.Dialogue:
                displayDialogueTextWithOptionAndPlay();
                break;
            case GameState.Option:
                break;
            case GameState.Combat:
                if (combatManager.CombatStatus != CombatStatus.InProgress)
                {
                    if (Input.GetKeyDown(KeyCode.B))
                    {
                        Enemy goblin = new Enemy("Goblin", 100, 0);

                        combatManager.StartCombat(goblin, Player);

                    }
                }
                break;
        }
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

        text.text = _currentAct.PrologueTextDescription + "\n\n Press [Enter] to continue...";
        audioManager.NarrativeSource.clip = _currentAct.AudioPrologue;
        audioManager.NarrativeSource.Play();

       // InputLayer = InputLayer.Narrative;
    }

    private void displayDialogueTextWithOptionAndPlay()
    {
        _currentDialogue = _currentAct.Dialogues[_currentDialogueIndex];
        if (_currentDialogue == null)
        {
            return;
        }

        string textOptions = "";

        for (int i = 0; i < _currentDialogue.Options.Length; i++)
        {
            textOptions += "\nOption " + (i+1) + ": " + _currentDialogue.Options[i];
        }

        text.text = _currentDialogue.TextDescription + "\n" + textOptions;
        audioManager.NarrativeSource.clip = _currentDialogue.AudioDescription;
        audioManager.NarrativeSource.Play();

        GameState = GameState.Dialogue;
        InputLayer = InputLayer.DialogueChoice;
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
