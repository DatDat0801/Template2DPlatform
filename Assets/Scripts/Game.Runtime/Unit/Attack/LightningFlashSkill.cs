using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Lean.Pool;
using UnityEngine;

namespace Game.Runtime
{
    public class LightningFlashSkill : UnitBaseSkill
    {
        private GameObject goLightingFlash;
        private LightningFlashSkillData _flashSkillData;

        public override void Innit(UnitSkillData skillData)
        {
            base.Innit(skillData);
            goLightingFlash = GameManager.instant.lightningFlash;
            this._flashSkillData = (LightningFlashSkillData)this._skillData;
        }

        public override void DoSkill(UnitBaseActionMng owner,Transform posExcute, List<UnitBaseActionMng> target)
        {
            base.DoSkill(owner,posExcute, target);
            
            if (_flashSkillData.bothSide)
            {
                DoSkillRight(posExcute);
                DoSkillLeft(posExcute);
            }
        }

        async UniTaskVoid DoSkillRight(Transform posExcute)
        {
            List<LightningFlash> flashes = new List<LightningFlash>();
            var tmp = posExcute.position;
            for (int i = 0; i < this._flashSkillData.quantity; i++)
            {
                tmp.x += this._flashSkillData.spaceX;
                var pos = Physics2D.ClosestPoint(tmp, GameManager.instant.groundColl);
                var go=LeanPool.Spawn(goLightingFlash, pos, goLightingFlash.transform.rotation);
                flashes.Add(go.GetComponent<LightningFlash>());
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            }

            foreach (var fl in flashes)
            {
                fl.Init(this._flashSkillData);
                fl.SetAttack();
            }
        }
        
        async UniTaskVoid DoSkillLeft(Transform posExcute)
        {
            List<LightningFlash> flashes = new List<LightningFlash>();
            var tmp = posExcute.position;
            for (int i = 0; i < this._flashSkillData.quantity; i++)
            {
                tmp.x -= this._flashSkillData.spaceX;
                var pos = Physics2D.ClosestPoint(tmp, GameManager.instant.groundColl);
                var go=LeanPool.Spawn(goLightingFlash, pos, goLightingFlash.transform.rotation);
                flashes.Add(go.GetComponent<LightningFlash>());
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            }

            foreach (var fl in flashes)
            {
                fl.Init(this._flashSkillData);
                fl.SetAttack();
            }
        }
    }
}