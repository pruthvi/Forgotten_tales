using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forgotten_Tale___The_Cult_of_Darkness
{
    class Engine
    {
        private List<Act> acts;

        public Engine()
        {

        }

        public void AddAct(params Act[] acts)
        {
            if (this.acts == null)
                this.acts = new List<Act>();

            this.acts.AddRange(acts);
        }

        public void Start()
        {
            if (acts == null || acts.Count == 0) throw new NullReferenceException("No avaliable act to start");

            foreach (Act act in acts)
            {
                Console.WriteLine($"Act {act.Id} - {act.Name}\n\n{act.Intro}\n");

                //Call this method to set start action of the current act to default
                act.SetUp();

                Action currentAction = act.StartAction;

                while (!act.ActEnds)
                {
                    //Print out the description of the current action
                    Console.WriteLine($"{currentAction.Id} - {currentAction.Description}\n");
                    //Print out the options of the current action
                    int i = 1;
                    foreach(Option option in currentAction.Options)
                    {
                        Console.WriteLine($"   {i++}. {option.Description} (Go to {option.NextActionId})");
                    }

                    Console.Write("\nSelect Your Option: ");

                    int selection = OptionInput();

                    currentAction = currentAction.NextAction(selection, act.Actions);

                    if (currentAction == null) act.ActEnds = true;

                    Console.WriteLine();
                }

                Console.WriteLine($"\nEnd of Act {act.Id}");
            }
        }

        //Return -1 if invalid selction
        public int OptionInput()
        {
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch
            {
                return -1;
            }
        }
    }
}
