using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Narrative
{
    class Tale
    {
        public IDictionary<string, Act> Acts { get; private set; }

        public string Name { get; private set; }

        public Act CurrentAct { get; private set; }

        public Tale(string name)
        {
            this.Name = name;
            this.Acts = new Dictionary<string, Act>();
        }

        public void Add(Act act)
        {
            if (!Acts.ContainsKey(act.Id))
            {
                Acts.Add(act.Id, act);
            }
            else
            {
                throw new ArgumentException("Act already exists in Tale.");
            }
        }

        public void SetCurrentAct(string actId)
        {
            if (!Acts.ContainsKey(actId))
            {
                throw new Exception("Act does not exist.");
            }
            else
            {
                CurrentAct = Acts[actId];
            }
        }
    }
}
