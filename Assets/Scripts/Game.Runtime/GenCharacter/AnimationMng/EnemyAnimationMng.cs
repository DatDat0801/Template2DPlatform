using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class EnemyAnimationMng : UnitAnimationMng
    {
        protected override void Init(AnimationDefination animationDefination = null)
        {
            _animationDefination = new AnimationDefination();
            this._animationDefination.run = "Walk";
            // this._animationDefination.attack_3 = "Attac";
           // base.Init(animationDefination);
        }
    }
}
