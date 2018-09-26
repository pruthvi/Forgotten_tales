using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forgotten_Tale___The_Cult_of_Darkness
{
    class Option
    {
        public string Description { get; private set; }
        public int NextActionId { get; private set; }

        public Option(string description, int nextActionId)
        {
            this.Description = description;
            this.NextActionId = nextActionId;
        }
    }
}
