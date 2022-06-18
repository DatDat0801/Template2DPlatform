using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace Game.Runtime
{
    public class HPItem : BaseItem
    {
        [SerializeField]private float hp;
        public override void DoItemEffect(UnitBaseActionMng target)
        {
            target.AddHealth(hp);
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag.CompareTo("Player")==0)
            {
                DoItemEffect(other.collider.GetComponent<UnitBaseActionMng>());
                LeanPool.Despawn(this);
            }
        }
    }
}
