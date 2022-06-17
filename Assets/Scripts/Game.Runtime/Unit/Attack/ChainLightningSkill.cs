using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace Game.Runtime
{
    public class ChainLightningSkill : UnitBaseSkill
    {
        private GameObject goSkillObject;

        public override void Innit(UnitSkillData skillData)
        {
            base.Innit(skillData);
            this.goSkillObject = GameManager.instant.goChainLightning;
        }

        public override void DoSkill(UnitBaseActionMng owner,Transform posExcute, List<UnitBaseActionMng> target)
        {
            base.DoSkill(owner,posExcute, target);
            if(target.Count==0) return;
            var go=LeanPool.Spawn(this.goSkillObject, target[0].transform.position, this.goSkillObject.transform.rotation);
            go.GetComponent<ChainLightning>().Init(this._skillData,owner.IsFacingRight);
        }
    }
}