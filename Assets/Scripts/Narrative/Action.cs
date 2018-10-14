using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Narrative
{
    [Serializable]
    public class Action : IReadable, IGameEvent, ISelectable
    {
        public enum SelectionStatus { InProgress, Completed }

        private static int nextActionId = 0;

        public string Id { get; private set; }

        [SerializeField]
        public List<Option> Options { get; private set; }

        public bool IsSelected { get; private set; }

        public Act Owner { get; private set; }

        public SelectionStatus Status { get; private set; }

        private int selectedOptionIndex = -1;
        public int SelectedOptionIndex {
            get
            {
                return selectedOptionIndex;
            }
            set
            {
                if(value > 0 && value <= Options.Count)
                {
                    Debug.Log("Selected " + value);
                    selectedOptionIndex = value - 1;
                    Status = SelectionStatus.Completed;
                }
            }
        }

        public Action(Act owner, string text)
        {
            this.Options = new List<Option>();
            this.Id = "a" + nextActionId++;
            this.Owner = owner;
            this.Text = text;
            this.Owner.AddAction(this);
            this.Status = SelectionStatus.InProgress;
            SelectedOptionIndex = -1;
        }
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

        public void AddOption(Option opt)
        {
            this.Options.Add(opt);
        }

        public string OptionsText
        {
            get
            {
                string result = "";
                int index = 1;
                foreach(Option op in Options)
                {
                    result += "\n" + index + ". " + op.Text;
                    index++;
                }
                return result;
            }
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
