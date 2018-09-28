using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DbZLib;

public class Narrator : MonoBehaviour {

    public Text text;

    private Novel novel;
	// Use this for initialization
	void Start () {
        novel = new Novel();
        Act act1 = new Act("Begin");

        act1.Intro = "This is the intro of act 1";
        act1.End = "This is the end of act 1";
        Dialogue d1 = new Dialogue(act1, "This is the dialogue 1");
        Dialogue d2 = new Dialogue(act1, "This is the  dialogue 2");
        Dialogue d3 = new Dialogue(act1, "This is the  dialogue3");
        Dialogue d4 = new Dialogue(act1, "This is the  dialogue 4");
        Dialogue d5 = new Dialogue(act1, "This is the  dialogue 5");
        Dialogue d6 = new Dialogue(act1, "This is the  dialogue 6 ");

        d1.AddOption(new Option("dialogue 1 option 1, this will bring you to d2", d2),
                     new Option("Dialogue 1 option 2, this will bring you to d5", d5));

        d2.AddOption(new Option("dialogue 1 option 1, this will bring you to d3", d3));

        d5.AddOption(new Option("dialogue 1 option 1, this will bring you to d6", d6));

        novel.AddAct(act1);

        //int i = 1;
        //foreach (Dialogue d in act1.Dialogues)
        //{
        //    Console.WriteLine($"(D{i}) {d.Text}");
        //    int i2 = 1;
        //    foreach (Option o in d.Options)
        //    {
        //        Console.WriteLine($"\t{i2}. {o.Text}");
        //        i2++;
        //    }
        //    i++;
        //}
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            text.text = novel.FirstAct.Intro;
        }
	}
}
