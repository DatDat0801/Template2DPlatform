using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Game.Runtime
{
    [Serializable]
    public class UnitBaseActionMng : MonoBehaviour, IUpdate,IHurt
    {
        protected UnitData unitData;

        public UnitData UnitData
        {
            get => this.unitData;
        }

        protected UnitStatus _unitStatus;

        public UnitStatus UnitStatus
        {
            get => this._unitStatus;
        }

        [SerializeField] protected bool _isFacingRight;

        public bool IsFacingRight
        {
            get => this._isFacingRight;
        }

        #region === Base Action ===

        protected MoveLeft _moveLeft;
        protected MoveRight _moveRight;
        protected Idle _idle;
        protected Rigidbody2D _rb;

        #endregion

        [SerializeField] protected UnitAnimationMng _unitAnimationMng;

        [Header("CheckGround")] 
        [SerializeField] protected Transform _pointGround;
        [SerializeField] protected float _radius;
        [SerializeField] protected LayerMask _whatIsGround;

        [Header("CheckAttackColl")] [SerializeField]
        protected UnitAttackBoxCollider _attackBoxCollider;

        [Header("Unit Stat")] [SerializeField] protected UnitBaseStatMng _unitBaseStatMng;
        [Header("Unit Skill")] [SerializeField] protected UnitSkillManager _unitSkillManager;

        public static UnitBaseActionMng inst;

        #region ==== event ====

        public Action<int,float> onUpdateHP;

        #endregion
        
        protected virtual void Start()
        {
            GameManager.instant.RegisterUpdate(this);
            if (inst == null) inst = this;
        }

        private void OnValidate()
        {
            this._unitSkillManager = GetComponent<UnitSkillManager>();
        }

        public virtual void Init(UnitData unitData)
        {
            if (this._moveLeft == null && gameObject.GetComponent<MoveLeft>() != null)
            {
                this._moveLeft = gameObject.GetComponent<MoveLeft>();
            }

            if (this._moveRight == null && gameObject.GetComponent<MoveRight>() != null)
            {
                this._moveRight = gameObject.GetComponent<MoveRight>();
            }

            if (this._idle == null && gameObject.GetComponent<Idle>() != null)
            {
                this._idle = gameObject.GetComponent<Idle>();
            }

            _rb = GetComponent<Rigidbody2D>();
            this.unitData = unitData;
            if (this._unitBaseStatMng != null)
            {
                this._unitBaseStatMng.Innit(unitData.unitStat);
            }
        }

        #region ==== Action Method ====

        public virtual void SetMoveLeft(float speed)
        {
            if (this._moveLeft == null) return;
            if (IsOnGround)
            {
                _unitAnimationMng.PlayRunAnim();
            }
            this._moveLeft.DoMoveLeft(speed);
        }

        public virtual void SetMoveRight(float speed)
        {
            if (this._moveRight == null) return;
            if (IsOnGround)
            {
                _unitAnimationMng.PlayRunAnim();
            }
            this._moveRight.DoMoveRight(speed);
        }

        public virtual void SetIdle()
        {
            if (this._idle == null) return;
            this._idle.DoIdle();
            _unitAnimationMng.PlayIdle();
        }

        public virtual void SetStop()
        {
            _rb.velocity = new Vector2(0, this._rb.velocity.y);
        }

        #endregion


        protected virtual void Controller()
        {
            
        }

        public virtual void DoUpdate()
        {
            Controller();
        }

        public virtual void FlipToRight(bool toRight = true)
        {
            Vector2 vt = transform.localScale;
            if (toRight)
            {
                vt.x = 1;
            }
            else
            {
                vt.x = -1;
            }

            this._isFacingRight = !toRight;
            transform.localScale = vt;
        }

        protected bool IsOnGround
        {
            get
            {
                return Physics2D.OverlapCircle(this._pointGround.position, this._radius, this._whatIsGround);
            }
        }

        public virtual bool GetHurt(float subHP)
        {
            if (this._unitStatus == UnitStatus.Death) return true;
            var hp = _unitBaseStatMng.SubHealth(subHP);
                onUpdateHP?.Invoke(this.unitData.id,hp);
            if (hp > 0)
            {
               this._unitAnimationMng.PlayHurt();
            }
            else
            {
                this._unitStatus = UnitStatus.Death;
                this._unitAnimationMng.PlayDeath();
                SetDeath();
                return true;
            }
            return false;
        }
        
        public virtual void AddHealth(float addHP)
        {
            if (this._unitStatus == UnitStatus.Death) return;
            var hp = _unitBaseStatMng.AddHealth(addHP);
            onUpdateHP?.Invoke(this.unitData.id,hp);
        }

        protected virtual void SetDeath()
        {
            
        }

        public List<int> GetListSkillID()
        {
            List<int> tmp = new List<int>();
            foreach (var skill in this._unitSkillManager.UnitSkills)
            {
                if((int)skill<0) continue;
                tmp.Add((int)skill);
            }

            return tmp;
        }
    }
}