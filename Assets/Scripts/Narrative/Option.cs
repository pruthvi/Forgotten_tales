using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Narrative
{
    [Serializable]
    public class Option : ISelectable, IReadable, IGameEvent
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

        public Action Owner;

        public Option(Action owner, string text, string nextActionId)
        {
            this.Owner = owner;
            this.Text = text;
            this.NextActionId = nextActionId;
            this.Owner.AddOption(this);
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
