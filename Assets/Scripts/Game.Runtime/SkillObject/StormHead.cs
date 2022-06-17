using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace Game.Runtime
{
    public class StormHead : MonoBehaviour
    {
        public Transform centerPoint;
        public Vector2 size;
        public LayerMask whatIsTarget;
        private List<UnitBaseActionMng> _unitBaseActionMngs;
        [SerializeField] private StormHeadAnim stormHeadAnim;

        private UnitSkillData _unitSkillData;

        public void Init(UnitSkillData unitSkillData)
        {
            this._unitSkillData = unitSkillData;
            stormHeadAnim.onAttackLeft = AttackLeft;
            stormHeadAnim.onAttackRight = AttackRight;
            stormHeadAnim.onDeath = OnDeath;
        }

        void Controller()
        {
            
        }

        Collider2D[] GetTargets
        {
            get
            {
                return Physics2D.OverlapBoxAll(this.centerPoint.position, this.size, 0, this.whatIsTarget);
            }
        }

        public void AttackLeft()
        {
            foreach (var target in GetTargets)
            {
                if (target.gameObject.transform.position.x <= transform.position.x)
                {
                    target.gameObject.GetComponent<UnitBaseActionMng>().GetHurt(this._unitSkillData.value);
                }
            }
        }

        public void AttackRight()
        {
            foreach (var target in GetTargets)
            {
                if (target.gameObject.transform.position.x >= transform.position.x)
                {
                    target.gameObject.GetComponent<UnitBaseActionMng>().GetHurt(this._unitSkillData.value);
                }
            }
        }

        public void OnDeath()
        {
            LeanPool.Despawn(this);
        }
    }
}