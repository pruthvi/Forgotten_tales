using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Narrative;
using Assets.Scripts.Utilities;
using Assets.Scripts.Configurations;

public class Narrator : MonoBehaviour {

    public Text text;

    public float InputInterval = 1;

    public NarratorSetting narratorSettings;

    public Act CurrentAct;

    private Timer inputTimer;

    // Use this for initialization
    void Start () {
        inputTimer = new Timer(InputInterval, true);
        Act act1 = new Act("Begin");

        act1.IntroDescription = "This is the intro of act 1";
        act1.EndDescription = "This is the end of act 1";

        Action a1 = new Action(act1, "This is the dialogue 1");
        Action a2 = new Action(act1, "This is the dialogue 2");
        Action a3 = new Action(act1, "This is the dialogue 3");
        Action a4 = new Action(act1, "This is the dialogue 4");
        Action a5 = new Action(act1, "This is the dialogue 5");

        Option a1o1 = new Option(a1, "this goes to 2", a2.Id);
        Option a1o2 = new Option(a1, "this goes to 3", a3.Id);
        Option a1o3 = new Option(a1, "this goes to 4", a4.Id);

        CurrentAct = act1;
    }
	
	// Update is called once per frame
	void Update () {
        //If the current act is not finished
        if (!CurrentAct.End)
        {
            //Update the timer
            inputTimer.Tick(Time.deltaTime);

            //If is able to input
            if (inputTimer.Ready)
            {
                //if act does not begin play the intro of this act and wait until it finishes(use Coroutine)
                if (!CurrentAct.Begin)
                {
                    //if return is pressed
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Debug.Log("Act Begin");
                        CurrentAct.BeginAct();
                        playAudio(CurrentAct.IntroDescription);
                        inputTimer.Reset();
                    }
                }
                else
                {
                    if (CurrentAct.End)
                    {
                        Debug.Log("Act End");
                        playAudio(CurrentAct.EndDescription);
                        inputTimer.Reset();
                    }
                    else
                    {
                        switch (CurrentAct.CurrentGameEvent.Type)
                        {
                            case GameEventType.Action:
                                Debug.Log("Checking");
                                Action action = (Action)CurrentAct.CurrentGameEvent;
                                if (action.Status == Action.SelectionStatus.InProgress)
                                {
                                    displayActionWithOption(action);
                                    //Check for user input
                                    action.SelectedOptionIndex = GetSelection();

                                    //Set it to next if selected option is valid
                                    if (action.Status == Action.SelectionStatus.Completed)
                                    {
                                        Debug.Log("Selection Completed");
                                        CurrentAct.MoveToNextEvent();
                                        inputTimer.Reset();
                                    }

                                }
                                break;
                            case GameEventType.Combat:
                                throw new System.NotImplementedException();
                        }
                    }
                }

                //Reset the input timer
            }
        }

        
       

    }

    void displayActionWithOption(Action action)
    {
        string result = "";
        for(int i = 0; i < action.Options.Count; i++)
        {
            result += "\n\t" + (i + 1) + ". " + action.Options[i].Text;
        }
        playAudio(action.Text + result);
    }

    public int GetSelection()
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

        return result;
    }

    private void playAudio(string txt)
    {
        text.text = txt;
    }
}
