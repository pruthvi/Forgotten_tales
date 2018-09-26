using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forgotten_Tale___The_Cult_of_Darkness
{
    class Act
    {
        public static int NextAvaliableAct { get; private set; } = 1;

        //Properties
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Intro { get; set; }
        public string End { get; set; }
        public Action StartAction { get; private set; }
        public bool ActEnds { get; set; }

        public List<Action> Actions { get; private set; }

        private static int nextAvaliableAction = 0;

        public Act(string name)
        {
            this.Id = NextAvaliableAct++;
            this.Name = name;
            this.Actions = new List<Action>();
        }

        public Action GetAction(int index)
        {
            int i = 0;
            foreach (Action action in Actions)
            {
                i++;

            }
            return null;
        }

        public void AddAction(params Action[] actions)
        {
            if (this.Actions == null) this.Actions = new List<Action>();

            foreach (Action action in actions)
            {
                this.Actions.Add(action);
            }
        }

        public void SetUp()
        {
            if (Actions == null || Actions.Count == 0) throw new Exception("No avaliable actions");

            if (StartAction == null)
            {
                if (Actions.Count > 0)
                {
                    StartAction = Actions[0];
                }
                else
                {
                    throw new NullReferenceException("No begin Action");
                }
            }
        }
    }
}
