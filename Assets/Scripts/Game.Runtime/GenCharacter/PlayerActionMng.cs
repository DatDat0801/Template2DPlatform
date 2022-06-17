using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerActionMng : UnitBaseActionMng
    {
        private Jump _jump;
        private CharacterData _characterData;

        public CharacterData CharacterData
        {
            get => this._characterData;
        }

        public override void Init(UnitData unitData)
        {
            base.Init(unitData);
            if (this._jump == null && gameObject.GetComponent<Jump>() != null)
            {
                this._jump = gameObject.GetComponent<Jump>();
            }

            this._characterData = (CharacterData)unitData;
        }

        public override void SetMoveLeft(float speed)
        {
            base.SetMoveLeft(speed);
            FlipToRight(false);
        }

        public override void SetMoveRight(float speed)
        {
            base.SetMoveRight(speed);
            FlipToRight(true);
        }

        public virtual void DoJump(float jumpForce)
        {
            this._jump.DoJump(jumpForce);
        }

        protected override void Controller()
        {
            base.Controller();
            if(this._unitStatus == UnitStatus.Death) return;
            if (Input.GetKey(KeyCode.D))
            {
                SetMoveRight(this.unitData.runSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                SetMoveLeft(this.unitData.runSpeed);
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                SetIdle();
                SetStop();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (IsOnGround)
                {
                    
                    DoJump(this._characterData.jumpForce);
                    this._unitAnimationMng.PlayJump();
                }
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                this._unitAnimationMng.onAttackSkill1 = OnExcuteSkill1;
                this._unitAnimationMng.PlaySkill1();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                this._unitAnimationMng.onAttackSkill2 = OnExcuteSkill2;
                this._unitAnimationMng.PlaySkill2();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                this._unitAnimationMng.onAttackSkill3 = OnExcuteSkill3;
                this._unitAnimationMng.PlaySkill3();
            }

            if (!IsOnGround)
            {
                this._unitAnimationMng.PlayJump();
            }
            else
            {
                if (this._unitAnimationMng.IsInAttack == false && this._unitAnimationMng.IsInHurt==false)
                {
                    if (Mathf.Abs(_rb.velocity.x) >= 0.3f)
                    {
                        this._unitAnimationMng.PlayRunAnim();
                    }
                    else
                    {
                        if (this._unitAnimationMng.IsInIdle == false)
                        {
                            this._unitAnimationMng.PlayIdle();
                        }
                    }
                }
            }
            
            
        }

        void OnExcuteSkill1()
        {
            var coll = this._attackBoxCollider.GetTarget();
            
            List<UnitBaseActionMng> tmp = new List<UnitBaseActionMng>();
            foreach (var unit in coll)
            {
                tmp.Add(unit.GetComponent<UnitBaseActionMng>());
            }
            if(tmp.Count==0) return;
            _unitSkillManager.DoSkill(0,this,transform,tmp);
        }
        
        void OnExcuteSkill2()
        {
            var coll = this._attackBoxCollider.GetTarget();
            List<UnitBaseActionMng> tmp = new List<UnitBaseActionMng>();
            foreach (var unit in coll)
            {
                tmp.Add(unit.GetComponent<UnitBaseActionMng>());
            }
            _unitSkillManager.DoSkill(1,this,transform,tmp);
        }
        
        void OnExcuteSkill3()
        {
            var coll = this._attackBoxCollider.GetTarget();
            List<UnitBaseActionMng> tmp = new List<UnitBaseActionMng>();
            foreach (var unit in coll)
            {
                tmp.Add(unit.GetComponent<UnitBaseActionMng>());
            }
            _unitSkillManager.DoSkill(2,this,transform,tmp);
        }

    }
}
