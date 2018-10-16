using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InputLayer { Narrative, CombatChoice, Combat }

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    private AudioManager audioManager;
    private CombatManager combatManager;
    public Player Player { get; private set; }

    public int MinSelectionInput = 1;
    public int MaxSelectionInput = 3;

    public InputLayer InputLayer { get; private set; }

    [SerializeField]
    public int LastSelectionInput { get; private set; }

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

        InitGame();
    }

    void InitGame()
    {
        Player = new Player("Main Character", 100, 50);
        Debug.Log("Player - " + Player.Name);

        combatManager = new CombatManager();
        LastSelectionInput = -1;
    }

    // Use this for initialization
    void Start()
    {
        
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
        // Update the last input

        updateLastInput();

        if (combatManager.CombatStatus != CombatStatus.InProgress)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Enemy goblin = new Enemy("Goblin", 100, 0);

                combatManager.StartCombat(goblin, Player);

            }
        }
    }

    private void updateLastInput()
    {

        switch (InputLayer)
        {
            // This layer only check the up/down keys
            case InputLayer.Narrative:
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    LastSelectionInput = LastSelectionInput - 1 <= MinSelectionInput ? MinSelectionInput : LastSelectionInput - 1;
                }

                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    LastSelectionInput = LastSelectionInput + 1 >= MaxSelectionInput ? MaxSelectionInput : LastSelectionInput + 1;
                }
                break;
            // This layer only check the 1/2/3 keys
            case InputLayer.CombatChoice:
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    LastSelectionInput = 1;
                }

                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    LastSelectionInput = 2;
                }

                if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    LastSelectionInput = 3;
                }
                break;
            case InputLayer.Combat:
                break;
        }
        
    }
}
