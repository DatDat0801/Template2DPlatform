using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Runtime
{
    public class EnemyActionMng : UnitBaseActionMng
    {
        private EnemyData _enemyData;
        

        public override void Init(UnitData unitData)
        {
            base.Init(unitData);
            this._enemyData = (EnemyData)this.unitData;
        }

        protected override void Controller()
        {
            base.Controller();
            FindPlayer();
        }

        public override void FlipToRight(bool toRight = true)
        {
            Vector2 vt = transform.localScale;
            if (toRight)
            {
                vt.x = -1;
            }
            else
            {
                vt.x = 1;
            }

            this._isFacingRight = !toRight;
            transform.localScale = vt;
        }

        public override void SetMoveLeft(float speed)
        {
            if (IsOnGround)
            {
                base.SetMoveLeft(speed);
            }
        }

        protected override void SetDeath()
        {
            base.SetDeath();
            DeathEffect();
        }

        async UniTaskVoid DeathEffect()
        {
            for (int i = 0; i < 5; i++)
            {
                this._unitAnimationMng.gameObject.SetActive(false);
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                this._unitAnimationMng.gameObject.SetActive(true);
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            }
            this._unitAnimationMng.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private bool usedToMoveToPlayer = false;
        protected virtual void FindPlayer()
        {
            if(this._unitStatus== UnitStatus.Death || this._unitAnimationMng.IsInHurt) return;
            if (Mathf.Abs(GameManager.instant.ActivePlayer.transform.position.x - transform.position.x)>this._enemyData.distanceStopBeforePlayer)
            {
                if (IsPlayerInTheLeft)
                {
                    SetMoveLeft(this._enemyData.runSpeed);
                    FlipToRight(false);
                }
                else
                {
                    SetMoveRight(this._enemyData.runSpeed);
                    FlipToRight();
                }

                usedToMoveToPlayer = true;
                this.isAttack = false;
            }
            else
            {
                if (this._unitAnimationMng.IsInIdle==false && usedToMoveToPlayer)
                {
                    this._unitAnimationMng.PlayIdle();
                    usedToMoveToPlayer = false;
                }

                if (this.isAttack == false && GameManager.instant.ActivePlayer.UnitStatus!=UnitStatus.Death)
                {
                    Attack();
                }
            }
        }

        private bool isAttack;
        async UniTaskVoid Attack()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            this._unitAnimationMng.onAttackSkill2 = OnExcuteSkill1;
            this._unitAnimationMng.PlaySkill2();
            this.isAttack = true;
        }

        bool IsPlayerInTheLeft
        {
            get
            {
                return GameManager.instant.ActivePlayer.transform.position.x < transform.position.x;
            }
        }
        
        public void OnExcuteSkill1()
        {
            var coll = this._attackBoxCollider.GetTarget();
            
            List<UnitBaseActionMng> tmp = new List<UnitBaseActionMng>();
            foreach (var unit in coll)
            {
                tmp.Add(unit.GetComponent<UnitBaseActionMng>());
            }
            if(tmp.Count==0) return;
            _unitSkillManager.DoSkill(0,this,transform,tmp);
            this.isAttack = false;
        }
    }
}
