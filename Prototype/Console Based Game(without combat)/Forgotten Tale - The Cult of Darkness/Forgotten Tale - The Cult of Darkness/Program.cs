using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forgotten_Tale___The_Cult_of_Darkness
{
    class Program
    {
        /*
            Act act01 = new Act("");
            act01.Intro = "";

            Action a1a0 = new Action("",
                                    new Option("", ),
                                    new Option("", ));
        */
        static void Main(string[] args)
        {

            Act act01 = new Act("Begin");
            act01.Intro = "Regulos, the bright jewel of Atmos." + 
            "A place for order where science and magic walk together.Among so many places in Regulos the most notorious, without a doubt, is the Arcane Academy." +
            "There, many talented minds study the mysteries of the science of magic."+
            "You are Arkantus, one of these students.Not so smart or gifted, you would not even be there if it were the kindness of Kalimari, the headmaster of the Arcane Academy."+
            "You are concentrated performing advanced alchemy experiments.There is a lot of research to be done and many of them were commissioned by the headmaster himself, Kalimari. The artifact you are examining is intriguing.Its resonance seems to nullify the magical properties of everything around him; he seems to be broken, as if some part of him were missing.After hours of research nothing seems made the artifact react to your experiments.Thus, the real property of this mysterious artifact remains unknown.";

            Action a1a0 = new Action("Then you hear someone knocking on the door. What are you going to do?",
                                    new Option("I go to the door and open it to know who was there", 1),
                                    new Option("I ignore the call. I need to finish this research as soon as possible", 2));
            Action a1a1 = new Action("When you open the door you find no one, just a note on the floor. The note says: \"Greetings Arkantus.I need you to meet me at the entrance to the Arcane Academy.I have the answers that you are looking for\". What are you going to do?",
                                    new Option("How silly, no one else knows about this artifact and what I'm doing here, everything is a secret. Must be some prank. I go back to my desk and continue my research. No distraction", 2),
                                    new Option("Who else could be involved in this? And why would not they talk to me directly inside of the Arcane Academy? I'm going to meet this mysterious person", 3));
            Action a1a2 = new Action("After a few minutes someone knock the door again, but this time much stronger, as if they were kicking the door. What are you going to do?",
                                    new Option("How absurd! This is a place of study, who could be doing this? I open the door to solve this.", 4),
                                    new Option("There is no time for distractions and pranks, I must finish the research requested by the headmaster Kalimari as soon as possible! No distraction!", 5));
            Action a1a3 = new Action("When you arrive outside, you expect the mysterious person reveal themselves and talk to you. But after an hour no one appeared. What are you going to do?",
                                    new Option("It must have been somebody's joke, I don’t believe I trusted in this. I'm going back to my office and continuing my research.", 9),
                                    new Option("I'm feeling being watched. There is crowd of people here, maybe the person is waiting for the place to be empty and some privacy. I'll wait.", 7));
            Action a1a4 = new Action("When you open the door you find no one, just another note on the floor. The note says: \"Greetings Arkantus.I need you to meet me at the entrance to the Arcane Academy.I have the answers that you are looking for\". What are you going to do?",
                                    new Option("How silly, no one else knows about this artifact and what I'm doing here, everything is a secret. Must be some prank. I go back to my desk and continue my research. No distraction", 5),
                                    new Option("Who else could be involved in this? And why would not they talk to me directly inside of the Arcane Academy? I'm going to meet this mysterious person.", 3));
            Action a1a5 = new Action("After a few minutes something violently breaks your window, and three spheres fall on the floor and then they explode, spreading a gray smoke, spit with horrible smell. You feel dizzy and you see someone in front of you. And that person is heading for your table. What are you going to do?",
                                    new Option("I sack my staff and attack with my magic the mysterious figure before it comes close to my research", 8),
                                    new Option("I run to look for help. Probably the explosion must have called attention and this threat can be very dangerous to take care of alone", 8));
            Action a1a6 = new Action("",
                                    new Option("", 1),
                                    new Option("", 2));
            Action a1a7 = new Action("It's almost nighttime, you're tired. Probably someone wanted to play a prank. You are very bored with yourself, for the time lost. And then you go back to your room to continue your research.",
                                    new Option("", 9));
            Action a1a8 = new Action("The silhouette of the mysterious figure disappears in the smoke. There is something strange about this smoke, you try to act as you decided, but your body is heavy, you fall on the ground and your senses are slowly going away, you try to fight more but is in vain, and then you faint.",
                                    new Option("You do not know how much time has passed, you regain your consciousness.", 9));
            Action a1a9 = new Action("Your room is destroyed; a horrible and nauseating smell is in the air. Your research and the mysterious artifact are gone. They left absolutely nothing. You have a terrible feeling. Who could be interested in this artifact to the point of doing so? You need to make a decision right now. What are you going to do?",
                                    new Option("There is no time to waste. I need to tell headmaster Kalimari what happened here. I'm going to his office.", 10),
                                    new Option("I need to keep my head cool. I going to look for evidence in the room and who could have done this.", 11));
            Action a1a10 = new Action("You arrive quickly in the headmaster's office, something strange is happening. You did not find anyone on the way. When you open the door, the room is empty. There is nobody. There is a large window in the headmaster's room, through it you see several flaming points going in the direction of the Arcane Academy. They are huge fireballs! What are you going to do?",
                                    new Option("I seek shelter immediately!", 12),
                                    new Option("I run to the exit of the Academy.", 13),
                                    new Option("[Local Teleportation Scroll] I use the Teleportation Scroll to get safely out of the Academy.", 14));
            Action a1a11 = new Action("The room is in complete chaos. The person who invaded took almost everything without a trace. There is only one local teleportation scroll, you keep it and run into the Kalimari headmaster's room",
                                    new Option("", 10));
            Action a1a12 = new Action("You quickly shelter yourself under the table when a big explosion hits the room. The Arcane Academy is under attack. Part of the ceiling collapses on top of your desk, trapping you underneath it. Someone enters the room and calls your name as I've been looking for you, worried. The person seems injured. What are you going to do?",
                                    new Option("I respond, and I ask for help.", 15),
                                    new Option("I am quiet in silence, I cannot trust anyone.", 16));
            Action a1a13 = new Action("You run away at full speed. When you arrive at the main hall of the Academy you are surprised by a huge ball of fire about to hit you. What do you do?",
                                    new Option("I use my protection magic. Arcane Barrier.", 17),
                                    new Option("I need to save mana. I jump to aside and with my hands I cover my head. (Player take 1-4 damage)", 17));
            Action a1a14 = new Action("You quickly pull the magic scroll and read the spell written. There is something different about this magic, as if someone interfered with its execution. You are surrounded by the magic of teleportation and disappear.",
                                    new Option("Hello Arkantus.My name is Kivan, I was sent by Kalimari to make sure you got safely out of this hell.\" There is much to be discussed and there is no time to lose, everything will be explained. Let's go!\"", 18));
            Action a1a15 = new Action("The mysterious figure helps you remove the stones blocking your path. He introduces himself:" +
                                    "- Arkantus, are you okay ? My name is Kivan, the headmaster Kalimari sent me to rescue you.There is a lot to be discussed and there is no time to waste.Let's go!" +
                                    "Together you both go to the exit.Already at the final gate there is a huge pile of fire."+
                                    " the fire pile seeming to be approaching ?"+
                                    "Wait!It's not a pile of fire, it's a fire elemental!Get ready for combat!",
                                    new Option("", 16));
            Action a1a15a = new Action("After an arduous battle, you finally get out of this place.",
                                    new Option("", 19));
            Action a1a16 = new Action("The smoke makes you cough and sob, which gives your position.",
                                    new Option("", 15));
            Action a1a17 = new Action("Your maneuver saves your life, but lots of smoke and wreckage on all sides. Somebody appears and quickly pulls you before another ball of fire hits you. He speaks:"+
                                    "- Quick, Arkantus.I was sent by Kalimari to rescue you, we have a lot of things to discuss, there is no time to lose.So, you two can safely escape",
                                    new Option("", 19));
            Action a1a18 = new Action("Away from all this confusion you see the Arcane Academy burning in flames. Hit by powerful and forbidden magic. There is no sign of who is responsible for this or how many people have been injured in this nefarious attack. You and your ally arrive in a tavern almost on the edge of town. There you find the headmaster Kalimari, also injured and another mysterious figure.",
                                    new Option("", -1));

            act01.AddAction(a1a0, a1a1, a1a2, a1a3, a1a4, a1a5, a1a6, a1a7, a1a8, a1a9,
                            a1a10, a1a11, a1a12, a1a13, a1a14, a1a15, a1a15a, a1a16, a1a17, a1a18);

            act01.End = "A few hours ago everything was in peace and now by a twist of fate everything will never be the same  ....";

            Engine engine = new Engine();

            engine.AddAct(act01);

            engine.Start();

            Console.ReadLine();
        }
    }
}
