using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime
{
    public class LightningFlash : MonoBehaviour
    {
        public Transform centerPoint;
        public Vector2 size;
        public LayerMask whatIsTarget;
        [SerializeField] private LightningFlashAnim lightningFlashAnim;

        private UnitSkillData _unitSkillData;

        public void Init(UnitSkillData unitSkillData)
        {
            this._unitSkillData = unitSkillData;
            this.lightningFlashAnim.onDeath = OnEnd;
            this.lightningFlashAnim.onAttack = OnAttack;
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

        public void SetAttack()
        {
            this.lightningFlashAnim.SetAttack();
        }

        public void OnAttack()
        {
            foreach (var target in GetTargets)
            {
                target.gameObject.GetComponent<UnitBaseActionMng>().GetHurt(this._unitSkillData.value);
            }
        }

        public void OnEnd()
        {
            LeanPool.Despawn(this);
        }
    }
}
