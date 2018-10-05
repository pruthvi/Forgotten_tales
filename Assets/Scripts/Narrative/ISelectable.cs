using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Narrative
{
    interface ISelectable
    {
        void OnHover();
        void OnConfirm();
    }
}
