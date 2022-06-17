using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime
{
    public class ChainLightning : MonoBehaviour
    {
        public Transform centerPoint;
        public float radius;
        public LayerMask whatIsTarget;
        [SerializeField] private ChainLightningAnim _chainLightningAnim;

        private UnitSkillData _unitSkillData;
        private GameObject goSkillObject;

        private bool _chainToRight;

        public void Init(UnitSkillData unitSkillData,bool chainToRight)
        {
            this._unitSkillData = unitSkillData;
            this._chainLightningAnim.onDeath = OnEnd;
            this._chainLightningAnim.onAttack = OnAttack;
            this.goSkillObject = GameManager.instant.goChainLightning;
            _chainToRight = chainToRight;
        }

        void Controller()
        {
        }

        Collider2D[] GetTargets
        {
            get
            {
                return Physics2D.OverlapCircleAll(this.centerPoint.position, this.radius, this.whatIsTarget);
            }
        }

        public void OnAttack()
        {
            foreach (var target in GetTargets)
            {
                var unit = target.gameObject.GetComponent<UnitBaseActionMng>();
                unit.GetHurt(this._unitSkillData.value);
                if (!this._chainToRight)
                {
                    if (target.transform.position.x > transform.position.x && unit.UnitStatus != UnitStatus.Death)
                    {
                        var go=LeanPool.Spawn(this.goSkillObject, target.transform.position, this.goSkillObject.transform.rotation);
                        go.GetComponent<ChainLightning>().Init(this._unitSkillData,this._chainToRight);
                        break;
                    }
                }
                else
                {
                    if (target.transform.position.x < transform.position.x && unit.UnitStatus != UnitStatus.Death)
                    {
                        var go=LeanPool.Spawn(this.goSkillObject, target.transform.position, this.goSkillObject.transform.rotation);
                        go.GetComponent<ChainLightning>().Init(this._unitSkillData,this._chainToRight);
                        break;
                    }
                }
            }
        }

        public void OnEnd()
        {
            LeanPool.Despawn(this);
        }
    }
}