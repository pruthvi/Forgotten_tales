using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IGameEvent
{
    // State Change Related
    void OnEnter();
    void OnExit();
    void OnUpdate();

    // GUI Related
    void OnGUIChange();

    // Input Related
    void OnInput();
}
