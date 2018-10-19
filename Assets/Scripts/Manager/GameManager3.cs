//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

////public enum NarrativeType { NarrativePrologue, NarrativeDialogue, NarrativeCombat, NarrativeCombatChoice }
//public enum GameProgress { Prologue, Intro, Dialogue, DialogueOption, EndOfAct }
//public class GameManager2 : MonoBehaviour
//{
//    public static GameManager Instance;

//    // Managers
//    public AudioManager AudioManager { get; private set; }
//    public SettingManager SettingManager { get; private set; }
//    public InputManager InputManager { get; private set; }
//    public CombatManager CombatManager { get; private set; }

//    // GUI
//    public Text textUI;
//    public Text textDescription;

//    private string textInstruction = "[R] - Replay | [T] - Replay Dialogue\n[F] Fast Forward | [Esc] Skip | ";

//    // Splash Screen
//    public bool SkipSplashScreen;
//    public AudioClip[] SplashScreenAudio;
//    public string[] SplashScreenText;
//    //private int _currentSplashScreenAudioIndex = 0;

//    // Controls
//    public string[] _controlItems;

//    // Game
//    public GameProgress GameProgress;
//    private GameState _gameState;
//    public GameState GameState
//    {
//        get
//        {
//            return _gameState;
//        }
//        private set
//        {
//            _gameState = value;
//            switch (value)
//            {
//                case GameState.InGame:
//                    
//                    UpdateInGameGUI();
//                    break;
//                case GameState.Battle:
//                    Debug.Log("hELLO");
//                    break;
//            }
//        }
//    }
//    private GameEvent _currentEvent;

//    private IGameState _currentGameState;

//    // Narrator
//    public Narrator Narrator { get; private set; }


//    // Player
//    public Player Player;
//    public void UpdateInventoryGUI()
//    {
//        if (Player.InventoryManager != null)
//        {
//            textDescription.alignment = TextAnchor.UpperLeft;
//            string items = "";
//            for (int i = 0; i < Player.InventoryManager.Items.Count; i++)
//            {
//                items += (InputManager.SelectedItemIndex == i ? "> " : "\t") + Player.InventoryManager.Items[i].name + " x " + Player.InventoryManager.Items[i].Quantity + "\n";
//            }
//        }
//    }
//    //IEnumerator playIntroAudio()
//    //{
//    //    for (int i = 0; i  < _currentAct.AudioIntros.Length; i++)
//    //    {
//    //        textDescription.text = _currentAct.IntroTextDescriptions[i];
//    //        Narrator.Play(_currentAct.AudioIntros[i]);
//    //        yield return new WaitForSeconds(_currentAct.AudioIntros[i].length * (1 / Narrator.Speed));
//    //    }
//    //    GameProgress = GameProgress.Dialogue;
//    //    Narrator.SetToIdle();
//    //}

//    public void NextEvent()
//    {

//        Debug.Log("Next Event");

//        //if (_gameManager.CurrentAct.NextEventBasedOnOption(SelectedItemIndex))
//        //{
//        //    InputStatus = InputStatus.Completed;
//        //    SelectionStatus = SelectionStatus.Valid;
//        //    _gameManager.Narrator.SetToIdle();
//        //    _gameManager.GameProgress = GameProgress.Dialogue;
//        //}
//    }
//}
