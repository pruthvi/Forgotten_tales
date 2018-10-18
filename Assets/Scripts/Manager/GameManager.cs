using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { SplashScreen, MainMenu, InGame, Controls, Settings, EndAct, EndGame }
//public enum NarrativeType { NarrativePrologue, NarrativeDialogue, NarrativeCombat, NarrativeCombatChoice }
public enum GameProgress { Prologue, Intro, Dialogue, DialogueOption, EndOfAct }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public AudioManager AudioManager { get; private set; }

    private CombatManager combatManager;
    public InputManager InputManager { get; private set; }
    public Player Player { get; private set; }

    // GUI
    public Text textUI;
    public Text textDescription;

    private string textInstruction = "[R] - Replay | [T] - Replay Dialogue\n[F] Fast Forward | [Esc] Skip | ";

    // Splash Screen
    public AudioClip[] SlpashScreenAudio;
    public string[] SplashScreenText;
    //private int _currentSplashScreenAudioIndex = 0;

    // Main Menu
    public AudioClip ClipMainMenu;
    public AudioClip[] ClipMenuItem;
    private string[] _menuItems = { "Start Game", "Controls", "Settings", "Exit" };

    // Intro
    private int _currentIntroClipIndex = 0;

    // Game
    public GameProgress GameProgress;
    private GameState _gameState;
    public GameState GameState
    {
        get
        {
            return _gameState;
        }
        private set
        {
            _gameState = value;
            switch (value)
            {
                case GameState.SplashScreen:
                    Narrator.SetToIdle();
                    textDescription.alignment = TextAnchor.MiddleCenter;
                    StartCoroutine(playSplashScreenAudio());
                    break;
                case GameState.MainMenu:
                    InputManager.SetMaxItemCount(_menuItems.Length);
                    AudioManager.PlayBGM(AudioManager.BGMMainMenuToIntro);
                    Narrator.SetToIdle();
                    textUI.text = "";
                    InputManager.ChangeInputLayer(InputLayer.MainMenu);
                    StartCoroutine(playMainMenuEnterAudio());
                    UpdateMainMenuGUI();
                    textDescription.alignment = TextAnchor.MiddleCenter;
                    break;
                case GameState.Controls:
                    Narrator.SetToIdle();
                    InputManager.ChangeInputLayer(InputLayer.Controls);
                    StartCoroutine(playControlsAudio());
                    UpdateControlsGUI();
                    textDescription.alignment = TextAnchor.MiddleCenter;
                    break;
                case GameState.Settings:
                    Narrator.SetToIdle();
                    InputManager.ChangeInputLayer(InputLayer.Settings);
                    textDescription.alignment = TextAnchor.UpperLeft;
                    UpdateSettingsGUI();
                    break;
                case GameState.InGame:
                    AudioManager.PlayBGM(AudioManager.BGMNarrative);
                    Narrator.SetToIdle();
                    InputManager.ChangeInputLayer(InputLayer.Dialogue);
                    textDescription.alignment = TextAnchor.UpperLeft;
                    UpdateInGameGUI();
                    break;
            }
        }
    }

    // Narrator
    public Narrator Narrator { get; private set; }

    // Act
    public Act[] Act;
    private Act _currentAct;
    public Act CurrentAct
    {
        get
        {
            return _currentAct;
        }
    }
    private int _currentActIndex = 0;
    private bool _actEnd;
    //private int _optionSelectedIndex = 0;

    

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

        InputManager = new InputManager(this);
        Narrator = new Narrator(AudioManager.NarrativeSource);

        //GameState = GameState.MainMenu; // For debug purpose, set back to splash screen when publish
        GameState = GameState.SplashScreen;
    }

    void initGame()
    {
        GameProgress = GameProgress.Prologue;
        _currentAct = Act[_currentActIndex];
        _currentActIndex = 0;
        // To reset current dialogue, if it was being stored
        _currentAct.Init();
        Narrator.Speed = 1;
    }

    public void StartGame()
    {
        GameState = GameState.InGame;
        initGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the Inputs
        InputManager.Update();
        
        // Update based on the state of the Game
        switch (GameState)
        {
        //    case GameState.SplashScreen:
        //        break;
            case GameState.MainMenu:
                break;
            case GameState.InGame:
                // Update Narrator Status
                Narrator.UpdateStatus();

                // Update Game Progress from Prologue to EndOfAct
                updateGameProgress();
             //   updateInGame();


                break;
            case GameState.Settings:
                UpdateSettingsGUI();
                break;
            case GameState.EndAct:
                // Check if there is more act, if not EndGame
                if (_currentActIndex + 1 < Act.Length)
                {
                    _currentAct = Act[_currentActIndex++];
                }
                else
                {
                    GameState = GameState.EndGame;
                }
                break;
            case GameState.EndGame:
                // Go back to main menu
                // ToDo: Add an option to allow user to go back to main menu with confirmation
                
                GameState = GameState.MainMenu;
                break;
        }
    }

    // Update the Main Menu GUI
    public void UpdateMainMenuGUI()
    {
        // Display each MenuItem
        string menu = "";
        for (int i = 0; i < _menuItems.Length; i++)
        {
            // Put > infront of the MenuItem if it was the SelectedIndex
            menu += (InputManager.SelectedItemIndex == i ? "> " : "\t") + _menuItems[i] + "\n";
        }
        textDescription.text = menu;

        // Play current selected item;
        Narrator.Play(ClipMenuItem[InputManager.SelectedItemIndex]);
    }

    // Update the Settings GUI
    public void UpdateSettingsGUI()
    {
        textUI.text = "Settings\n[Esc] Back to Menu";
        textDescription.text = "";
    }

    // Update Controls Submenu GUI
    public void UpdateControlsGUI()
    {
        textUI.text = "Controls\n[Esc] Back to Menu";
        textDescription.text = "";
    }

    // Update InGame GUI
    public void UpdateInGameGUI()
    {
        textUI.text = textInstruction + "Speed: x" + Narrator.SpeedModifier;
    }

    private void updateGameProgress()
    {
        switch (GameProgress)
        {
            case GameProgress.Prologue:
                if (Narrator.IsIdle)
                {
                    textDescription.text = _currentAct.PrologueTextDescription;
                    Narrator.Play(_currentAct.AudioPrologue);
                    InputManager.ChangeInputLayer(InputLayer.Dialogue);
                }
                if (Narrator.CompletedOrSkipped)
                {
                    GameProgress = GameProgress.Intro;
                    Narrator.SetToIdle();
                }
                break;
            case GameProgress.Intro:
                if (Narrator.IsIdle)
                {
                    textDescription.text = _currentAct.IntroTextDescriptions[_currentIntroClipIndex];
                    Narrator.Play(_currentAct.AudioIntros[_currentIntroClipIndex]);
                }
                if (Narrator.CompletedOrSkipped)
                {
                    if (_currentIntroClipIndex + 1 < _currentAct.AudioIntros.Length)
                    {

                        _currentIntroClipIndex++;
                        Narrator.SetToIdle();
                    }
                    else
                    {
                        GameProgress = GameProgress.Dialogue;
                        InputManager.ChangeInputLayer(InputLayer.Dialogue);
                        Narrator.SetToIdle();
                    }
                }
                break;
            case GameProgress.Dialogue:
                if (Narrator.IsIdle)
                {
                    textDescription.text = _currentAct.CurrentDialogue.TextDescription;
                    Narrator.Play(_currentAct.CurrentDialogue.AudioDescription, PlayType.Dialogue);
                    InputManager.ChangeInputLayer(InputLayer.Dialogue);
                }
                if (Narrator.CompletedOrSkipped)
                {
                    if (_currentAct.CurrentDialogue.IsEndOfAct)
                    {
                        GameState = GameState.EndAct;
                        return;
                    }
                    // If there is option prompt for input
                    if (_currentAct.CurrentDialogue.HasOptions)
                    {
                        GameProgress = GameProgress.DialogueOption;
                        InputManager.SetMaxItemCount(_currentAct.CurrentDialogue.Options.Count);
                        Narrator.Play(_currentAct.CurrentDialogue.Options[InputManager.SelectedItemIndex].AudioDescription, PlayType.Option);
                        UpdateDialogueWithOptionGUI();
                        InputManager.ChangeInputLayer(InputLayer.ChooseDialogueOption);
                    }
                    else
                    {
                        _currentAct.NextDialogue();
                        GameProgress = GameProgress.Dialogue;
                        Narrator.SetToIdle();
                    }
                }
                break;
            case GameProgress.DialogueOption:
                break;
        }
    }

    public void UpdateDialogueWithOptionGUI()
    {
        string options = "";
        for (int i = 0; i < _currentAct.CurrentDialogue.Options.Count; i++)
        {
            options += (InputManager.SelectedItemIndex == i ? "> " : "\t") + " Option " + (i + 1) + ": \n\t\t" + _currentAct.CurrentDialogue.Options[i].TextDescription + "\n";
        }
        textDescription.text = _currentAct.CurrentDialogue.TextDescription + "\n" + options;
        if (InputManager.SelectedItemIndex < _currentAct.CurrentDialogue.Options.Count)
        {
            Narrator.Play(_currentAct.CurrentDialogue.Options[InputManager.SelectedItemIndex].AudioDescription, PlayType.Option);
        }
    }

    ///* Switch to next GameEvent
    //    Dialogue -> Dialogue
    //    Dialogue -> Combat
    //*/
    //public void Next()
    //{
    //    switch (_currentEvent.EventType)
    //    {
    //        case EventType.Dialogue:
    //            //   Dialogue dialogue = _currentEvent;                           
    //            break;
    //        case EventType.Combat:
    //            // Combat eventCombat = (Combat) _currentEvent;
    //            // switch(eventCombat.CombatResult)
    //            // win
    //            // _currentEvent = eventCombat.WinEvent;
    //            // lost
    //            //_currentEvent = eventCombat.LostEvent;
    //            break;
    //    }
    //}

    public void ChangeState(GameState state)
    {
        GameState = state;
    }

    IEnumerator playSplashScreenAudio()
    {
        for (int i = 0; i < SlpashScreenAudio.Length; i++)
        {
            textDescription.text = SplashScreenText[i];
            Narrator.Play(SlpashScreenAudio[i]);
            yield return new WaitForSeconds(SlpashScreenAudio[i].length * (1 / Narrator.Speed));
        }

        GameState = GameState.MainMenu;
    }

    IEnumerator playMainMenuEnterAudio()
    {
        Narrator.Play(ClipMainMenu);
        yield return new WaitForSeconds(ClipMainMenu.length);
    }

    //IEnumerator playIntroAudio()
    //{
    //    for (int i = 0; i  < _currentAct.AudioIntros.Length; i++)
    //    {
    //        textDescription.text = _currentAct.IntroTextDescriptions[i];
    //        Narrator.Play(_currentAct.AudioIntros[i]);
    //        yield return new WaitForSeconds(_currentAct.AudioIntros[i].length * (1 / Narrator.Speed));
    //    }
    //    GameProgress = GameProgress.Dialogue;
    //    Narrator.SetToIdle();
    //}

    IEnumerator playControlsAudio()
    {
        yield return new WaitForSeconds(1);
    }
}
