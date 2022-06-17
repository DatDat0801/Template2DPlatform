using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace Game.Runtime
{
    public class SummonStormHeadSkill : UnitBaseSkill
    {
        public override void DoSkill(UnitBaseActionMng owner,Transform posExcute, List<UnitBaseActionMng> target)
        {
            base.DoSkill(owner,posExcute, target);
            var pos= Physics2D.ClosestPoint(posExcute.transform.position, GameManager.instant.groundColl);
            GameObject obj= LeanPool.Spawn(GameManager.instant.headStorm,pos,GameManager.instant.headStorm.transform.rotation);
            obj.GetComponent<StormHead>().Init(this._skillData);
        }
    }
}
