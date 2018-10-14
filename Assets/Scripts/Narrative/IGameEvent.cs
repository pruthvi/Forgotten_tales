using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Narrative
{
    public enum GameEventType { Action, Option, Combat }

    public interface IGameEvent
    {
        GameEventType Type { get; }
        //IGameEvent NextEvent { get; }
    }
}
