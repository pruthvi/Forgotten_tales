using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Narrative
{
    public class Act : ISelectable
    {
        public static int NextAvaliableAct { get; private set; }

        //Properties
        public string Id { get; private set; }
        public string Name;
        public string IntroDescription;
        public string EndDescription;
        
        public IDictionary<string, Action> Actions { get; private set; }
        private List<string> idList;

        private static int nextAvaliableAction = 0;

        public bool Begin { get; set; }
        public bool End { get; set; }



        public bool SelectingOption { get; private set; }


        public bool firstBegin;

        public Act(string name)
        {
            this.Actions = new Dictionary<string, Action>();
            this.idList = new List<string>();
            this.Id = "act" + ++NextAvaliableAct;
            this.Name = name;
        }

        public void AddAction(Action action)
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
                    Debug.Log(idList.Count);
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

        public void MoveToNextEvent()
        {
            switch (CurrentGameEvent.Type)
            {
                case GameEventType.Action:
                    Action action = (Action)CurrentGameEvent;
                    if (action.Status == Action.SelectionStatus.Completed)
                    {
                        Debug.Log("Next id - " + action.Options[action.SelectedOptionIndex].NextActionId);
                        CurrentGameEvent = Actions[action.Options[action.SelectedOptionIndex].NextActionId];
                    }
                    break;
                case GameEventType.Combat:
                    break;
            }
        }



        public void UpdateSelection()
        {
            switch (CurrentGameEvent.Type)
            {
                case GameEventType.Action:
                    Debug.Log("Compeleted Selection");
                    Action action = (Action)CurrentGameEvent;
                    if (action.Status == Action.SelectionStatus.Completed)
                    {
                        Debug.Log("Next id - " + action.Options[action.SelectedOptionIndex - 1].NextActionId);
                        CurrentGameEvent = Actions[action.Options[action.SelectedOptionIndex - 1].NextActionId];
                    }
                    break;
                case GameEventType.Combat:
                    break;
            }
        }

        public int SelectedOption { get; set; }


        public void BeginAct() {
            firstBegin = true;
            Begin = true;
            CurrentGameEvent = FirstAction;
        }

        public IGameEvent CurrentGameEvent
        {
            get;
            private set;
        }
    }
}
