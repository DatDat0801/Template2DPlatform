using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    // normal attack duoc coi nhu 1 skill
    [Serializable]
    public class UnitBaseSkill : ISkill
    {
        [SerializeField]protected UnitSkillData _skillData;
        public virtual void Innit(UnitSkillData skillData)
        {
            this._skillData = skillData;
        }
        public virtual void DoSkill(UnitBaseActionMng owner, Transform posExcute, List<UnitBaseActionMng> target)
        {
            
        }
    }
}
