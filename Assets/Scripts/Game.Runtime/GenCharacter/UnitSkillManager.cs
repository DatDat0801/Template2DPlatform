using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Runtime
{
    public class UnitSkillManager : MonoBehaviour
    {
        [SerializeField] protected List<UnitSkill> _unitSkills;

        public List<UnitSkill> UnitSkills
        {
            get => this._unitSkills;
        }

        protected List<UnitBaseSkill> _unitBaseSkills = new List<UnitBaseSkill>();
        private UnitSkillData[] _skillDatas;
        
        private void Start()
        {
            Init();
        }

        public void Init()
        {
            this._skillDatas= GetSkillData();
            for (int i = 0; i < this._unitSkills.Count; i++)
            {
                CreateSkill(this._unitSkills[i]);
            }
        }
        
        void CreateSkill(UnitSkill unitSkill)
        {
            UnitSkillData dt = ScriptableObject.CreateInstance<UnitSkillData>();
            foreach (var skillData in _skillDatas)
            {
                if (unitSkill == skillData.unitSkill)
                {
                    dt = skillData;
                    break;
                }
            }
            switch (unitSkill)
            {
                case UnitSkill.MeleeAttack:
                    MeleeAttack melleAttack = new MeleeAttack();
                    melleAttack.Innit(dt);
                    this._unitBaseSkills.Add(melleAttack);
                    break;
                case UnitSkill.WoodExplosion:
                    WoodExplosionSkill woodExplosionSkill = new WoodExplosionSkill();
                    woodExplosionSkill.Innit(dt);
                    this._unitBaseSkills.Add(woodExplosionSkill);
                    break;
                case UnitSkill.SummonStormHead:
                    SummonStormHeadSkill summonStormHeadSkill = new SummonStormHeadSkill();
                    summonStormHeadSkill.Innit(dt);
                    this._unitBaseSkills.Add(summonStormHeadSkill);
                    break;
                case UnitSkill.LightningFlash:
                    LightningFlashSkill lightningFlashSkill = new LightningFlashSkill();
                    lightningFlashSkill.Innit(dt);
                    this._unitBaseSkills.Add(lightningFlashSkill);
                    break;
                case UnitSkill.ChainLightning:
                    ChainLightningSkill chainLightningSkill = new ChainLightningSkill();
                    chainLightningSkill.Innit(dt);
                    this._unitBaseSkills.Add(chainLightningSkill);
                    break;

            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idSkill">thu tu cua skill trong list skill</param>
        /// <param name="posExcute">diem thuc hien skill</param>
        /// <param name="target"></param>
        public virtual void DoSkill(int idSkill,UnitBaseActionMng owner,Transform posExcute, List<UnitBaseActionMng> target)
        {
            if (idSkill >= this._unitBaseSkills.Count)
            {
                return;
            }
            this._unitBaseSkills[idSkill].DoSkill(owner,posExcute,target);
        }

        UnitSkillData[] GetSkillData()
        {
            return Resources.LoadAll<UnitSkillData>($"SkillData/");
        }
    }
}
