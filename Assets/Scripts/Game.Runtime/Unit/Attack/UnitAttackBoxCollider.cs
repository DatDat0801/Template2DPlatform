using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Runtime
{
    public class UnitAttackBoxCollider : MonoBehaviour
    {
        [SerializeField] protected Vector2 boxSize;
        [SerializeField] protected LayerMask whatIsTarget;

        public Collider2D[] GetTarget()
        {
            return  Physics2D.OverlapBoxAll(transform.position, this.boxSize,0,whatIsTarget);
        }
    }
}
