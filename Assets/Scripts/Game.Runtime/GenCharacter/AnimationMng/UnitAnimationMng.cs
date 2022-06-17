using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    [Serializable]
    public class AnimationDefination
    {
        public string idle = "Idle";
        public string walk = "Walk";
        public string run = "Run";
        public string jump_1 = "Jump_1";
        public string hurt = "Hurt";
        public string death = "Death";
        public string attack_1 = "Attack_1";
        public string attack_2 = "Attack_2";
        public string attack_3 = "Attack_3";
    }

    public class UnitAnimationMng : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        protected AnimationDefination _animationDefination;
        private string _currentStatus="";

        #region ==== anim event ====

        public Action onAttackSkill1;
        public Action onAttackSkill2;
        public Action onAttackSkill3;

        #endregion

        public string CurrentStatus
        {
            get => this._currentStatus;
            set => this._currentStatus = value;
        }

        private void Start()
        {
            Init();
        }

        protected virtual void Init(AnimationDefination animationDefination = null)
        {
            if (animationDefination == null) _animationDefination = new AnimationDefination();
            this._currentStatus = this._animationDefination.idle;
        }

        void SetCurrentStatus(string status)
        {
            this.animator.SetBool(_currentStatus, false);
            _currentStatus = status;
        }

        public virtual void PlayRunAnim()
        {
            SetCurrentStatus(this._animationDefination.run);
            this.animator.SetTrigger(this._animationDefination.run);
        }

        public virtual void PlayIdle()
        {
            SetCurrentStatus(this._animationDefination.idle);
            this.animator.SetBool(this._animationDefination.idle, true);
        }

        public virtual void PlayJump()
        {
            SetCurrentStatus(this._animationDefination.jump_1);
            this.animator.SetBool(this._animationDefination.jump_1, true);
        }

        public virtual void PlaySkill1()
        {
            SetCurrentStatus(this._animationDefination.attack_1);
            this.animator.SetBool(this._animationDefination.attack_1, true);
        }

        public virtual void PlaySkill2()
        {
            SetCurrentStatus(this._animationDefination.attack_2);
            this.animator.SetBool(this._animationDefination.attack_2, true);
        }
        
        public virtual void PlaySkill3()
        {
            SetCurrentStatus(this._animationDefination.attack_3);
            this.animator.SetBool(this._animationDefination.attack_3, true);
        }

        public virtual void PlayHurt()
        {
            if(this._currentStatus.CompareTo(this._animationDefination.hurt)==0) return;
            SetCurrentStatus(this._animationDefination.hurt);
            this.animator.SetBool(this._animationDefination.hurt, true);
        }

        public virtual void PlayDeath()
        {
            SetCurrentStatus(this._animationDefination.death);
            this.animator.SetBool(this._animationDefination.death, true);
        }

        public void OnAttackSkill1()
        {
            this.onAttackSkill1?.Invoke();
        }

        public void OnAttackSkill2()
        {
            this.onAttackSkill2?.Invoke();
        }

        public void OnAttackSkill3()
        {
            this.onAttackSkill3?.Invoke();
        }

        #region ==== Check Status ====

        public bool IsInIdle
        {
            get
            {
                return this._currentStatus.CompareTo(this._animationDefination.idle) == 0;
            }
        }
        
        public bool IsInHurt
        {
            get
            {
                return this._currentStatus.CompareTo(this._animationDefination.hurt) == 0;
            }
        }

        public bool IsInAttack
        {
            get
            {
                return this._currentStatus.CompareTo(this._animationDefination.attack_1) == 0 ||
                       this._currentStatus.CompareTo(this._animationDefination.attack_2) == 0 ||
                       this._currentStatus.CompareTo(this._animationDefination.attack_3) == 0;
            }
        }

        #endregion
    }
}