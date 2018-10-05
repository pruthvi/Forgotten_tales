using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Narrative
{
    class Option : ISelectable, IReadable, IGameEvent
    {
        public string NextActionId { get; private set; }

        public string Text
        {
            get;
            private set;
        }

        public GameEventType Type
        {
            get
            {
                return GameEventType.Option;
            }
        }

        public IGameEvent NextEvent
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Option(string text, string nextActionId)
        {
            this.Text = text;
            this.NextActionId = nextActionId;
        }

        public void OnHover()
        {
            throw new NotImplementedException();
        }

        public void OnConfirm()
        {
            throw new NotImplementedException();
        }
    }
}
