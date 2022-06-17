using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class MeleeAttack : UnitBaseSkill
    {
        public override void Innit(UnitSkillData skillData)
        {
            base.Innit(skillData);
        }

        public override void DoSkill(UnitBaseActionMng owner, Transform posExcute, List<UnitBaseActionMng> target)
        {
            base.DoSkill(owner, posExcute, target);
            if(target.Count==0) return;
            if(target[0] == null) return;
            target[0].GetHurt(this._skillData.value);
        }
    }
}