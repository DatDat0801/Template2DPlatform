using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public interface IItem
    {
        void DoItemEffect(UnitBaseActionMng target);
    }
}
