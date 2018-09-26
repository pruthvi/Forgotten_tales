using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forgotten_Tale___The_Cult_of_Darkness
{
    class Action
    {
        public static int NextAvaliableAction { get; private set; } = 0;
        public string Id { get; private set; }
        public string Description { get; private set; }
        public List<Option> Options { get; private set; }

        public Action(string description, params Option[] options)
        {
            this.Id = "a" + Act.NextAvaliableAct + "a" + Action.NextAvaliableAction++;
            this.Description = description;

            Options = new List<Option>();

            Options.AddRange(options);
        }

        public Action NextAction(int optionIndex, List<Action> actions)
        {
            Option option;
            try
            {
                 option = Options[optionIndex-1];
            }
            catch(IndexOutOfRangeException)
            {
                throw new Exception("Invalid option");
            }

            try
            {
                if (option != null)
                {
                    if (option.NextActionId == -1)
                    {
                        return null;
                    }
                    else
                    {
                        return actions[option.NextActionId];
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Invalid next action");
            }
            return null;
        }
    }
}
