using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Narrative
{
    class Action : IReadable, IGameEvent, ISelectable
    {
        private static int nextActionId = 0;

        public string Id { get; private set; }
        public List<Option> Options { get; private set; }

        public bool IsSelected { get; private set; }

        public Act Owner { get; private set; }

        public Action(Act owner, string text)
        {
            this.Id = "a" + nextActionId++;
            this.Owner = owner;
            this.Text = text;
            this.Owner.Add(this);
        }

        //public Action NextAction(int optionIndex, List<Action> actions)
        //{
        //    Option option;
        //    try
        //    {
        //        option = Options[optionIndex - 1];
        //    }
        //    catch (IndexOutOfRangeException)
        //    {
        //        throw new Exception("Invalid option");
        //    }

        //    try
        //    {
        //        if (option != null)
        //        {
        //            if (option.NextActionId == -1)
        //            {
        //                return null;
        //            }
        //            else
        //            {
        //                return actions[option.NextActionId];
        //            }
        //        }
        //    }
        //    catch (IndexOutOfRangeException)
        //    {
        //        throw new Exception("Invalid next action");
        //    }
        //    return null;
        //}

        public void OnHover()
        {
            throw new NotImplementedException();
        }

        public void OnConfirm()
        {
            throw new NotImplementedException();
        }

        public string Text
        {
            get;
            private set;
        }

        public IGameEvent NextEvent
        {
            get
            {
                throw new NotImplementedException();
            }

            private set
            {

            }
        }

        public GameEventType Type
        {
            get
            {
                return GameEventType.Action;
            }
        }
    }
}
