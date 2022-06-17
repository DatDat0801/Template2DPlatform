using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    [Serializable]
    public class WoodExplosionSkill : UnitBaseSkill
    {
        public override void Innit(UnitSkillData skillData)
        {
            base.Innit(skillData);
        }

        public override void DoSkill(UnitBaseActionMng owner,Transform posExcute, List<UnitBaseActionMng> target)
        {
            base.DoSkill(owner,posExcute, target);
            SummonStormHeadSkill summonStormHeadSkill = new SummonStormHeadSkill();
            summonStormHeadSkill.Innit(this._skillData);
            summonStormHeadSkill.DoSkill(owner,posExcute,target);
        }
    }
}
