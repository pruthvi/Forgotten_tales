using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Narrative;
using Assets.Scripts.Utilities;

public class Narrator : MonoBehaviour {

    public Text text;

    public float SelectionInterval = 1;

    private Tale tale;

    private Timer selectionTimer;

	// Use this for initialization
	void Start () {
        selectionTimer = new Timer(SelectionInterval);

        tale = new Tale("Forgotten Tale - The Cult of Darkness");

        Act act1 = new Act(tale, "Begin");

        act1.Intro = "This is the intro of act 1";
        act1.End = "This is the end of act 1";

        Action a1 = new Action(act1, "This is the dialogue 1");
        Action a2 = new Action(act1, "This is the dialogue 2");
        Action a3 = new Action(act1, "This is the dialogue 3");
        Action a4 = new Action(act1, "This is the dialogue 4");
        Action a5 = new Action(act1, "This is the dialogue 5");

        tale.SetCurrentAct("act1");
    }
	
	// Update is called once per frame
	void Update () {

        //If the current act is not finished
        if (!tale.CurrentAct.End)
        {
            //Update the timer
            selectionTimer.Tick(Time.deltaTime);

            //If able to select and return is pressed
            if (selectionTimer.Ready && Input.GetKeyDown(KeyCode.Return))
            {



                //if act does not begin play the intro of this act and wait until it finishes(use Coroutine)
                if (!tale.CurrentAct.Begin)
                {
                    playAudio(tale.CurrentAct.IntroDescription);
                    tale.CurrentAct.Begin = true;
                }
                else
                {
                    if (tale.CurrentAct.End)
                    {
                        playAudio(tale.CurrentAct.EndDescription);
                    }
                    else
                    {
                        //Here is the game loop
                        /*
                            1. Get the next event(it can be action/option or combat)
                        */

                        IGameEvent cge = currentAct.CurrentGameEvent;

                        switch (cge.Type)
                        {
                            //If is action we play the text first, then 
                            case GameEventType.Action:
                                playAudio(((Action)cge).Text);
                                break;
                            case GameEventType.Option:
                                playAudio(((Option)cge).Text);
                                break;
                            case GameEventType.Combat:
                                //playAudio(((Action)cge).Text);
                                break;
                        }

                        currentAct.NextEvent();
                    }
                }



                //Reset the selection timer
                selectionTimer.Reset();
            }
        }
	}
    
    private void playAudio(string txt)
    {
        text.text = txt;
    }
}
