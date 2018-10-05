using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Narrative
{
    class Act : ISelectable
    {
        public static int NextAvaliableAct { get; private set; }

        //Properties
        public Tale Owner { get; private set; }
        public string Id { get; private set; }
        public string Name { get; set; }
        public string IntroDescription { get; set; }
        public string EndDescription { get; set; }
        //public bool ActEnds { get; set; }

        public IDictionary<string, Action> Actions { get; private set; }
        private List<string> idList;

        private static int nextAvaliableAction = 0;

        public bool Begin { get; set; }
        public bool End { get; set; }

        public bool SelectingOption { get; private set; }

        public Act(Tale owner, string name)
        {
            this.Id = "act" + ++NextAvaliableAct;
            this.Owner = owner;
            this.Owner.Add(this);
            this.Name = name;
            this.Actions = new Dictionary<string, Action>();
            this.idList = new List<string>();
        }

        public void Add(Action action)
        {
            if (!Actions.ContainsKey(action.Id))
            {
                Actions.Add(action.Id, action);
                idList.Add(action.Id);
            }
        }

        public void OnHover()
        {
            throw new NotImplementedException();
        }

        public void OnConfirm()
        {
            throw new NotImplementedException();
        }

        public Action FirstAction
        {
            get
            {
                if (idList.Count == 0)
                {
                    return null;
                }
                return Actions[idList[0]];
            }
        }

        public Action LastAction
        {
            get
            {
                if (idList.Count == 0)
                {
                    return null;
                }
                return Actions[idList[idList.Count - 1]];
            }
        }

        public void NextEvent()
        {
            if (!ActBegin)
            {
                CurrentGameEvent = FirstAction;
                ActBegin = true;
            }
            else
            {
                switch (CurrentGameEvent.Type)
                {
                    case GameEventType.Action:
                        CurrentGameEvent = ((Action)CurrentGameEvent).Options[SelectedOption];
                        break;
                    case GameEventType.Option:
                        CurrentGameEvent = Actions[((Option)CurrentGameEvent).NextActionId];
                        break;
                    case GameEventType.Combat:
                        break;
                }
            }
        }

        public int SelectedOption { get; set; }

        public IGameEvent CurrentGameEvent
        {
            get;
            private set;
        }
    }
}
