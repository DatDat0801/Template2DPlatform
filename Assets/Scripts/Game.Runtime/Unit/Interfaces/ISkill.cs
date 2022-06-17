using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public interface ISkill
    {
        void DoSkill(UnitBaseActionMng owner,Transform posExcute, List<UnitBaseActionMng> target);
    }
}