using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class BaseItem :MonoBehaviour, IItem
    {
        public virtual void DoItemEffect(UnitBaseActionMng target)
        {
            
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {

        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            
        }
    }
}
