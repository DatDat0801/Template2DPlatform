using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class UnitBaseStatMng : MonoBehaviour
    {
        private UnitBaseStat _startUnitBaseStat;
        private UnitBaseStat _currentUnitBaseStat;
        [SerializeField] protected HealthBarMng _healthBarMng;

        public void Innit(UnitBaseStat stat)
        {
            this._startUnitBaseStat = stat;
            this._currentUnitBaseStat = stat;
        }

        public float SubHealth(float sub)
        {
            this._currentUnitBaseStat.hp -= sub;
            if (this._currentUnitBaseStat.hp < 0)
            {
                this._currentUnitBaseStat.hp = 0;
            }
            this._healthBarMng.UpdateHealthBar(GetHPRatio);
            return this._currentUnitBaseStat.hp;
        }
        
        public void AddHealth(float add)
        {
            this._currentUnitBaseStat.hp += add;
            if (this._currentUnitBaseStat.hp > this._startUnitBaseStat.hp)
            {
                this._currentUnitBaseStat.hp = this._startUnitBaseStat.hp;
            }
            this._healthBarMng.UpdateHealthBar(GetHPRatio);
        }

        float GetHPRatio
        {
            get
            {
                return (this._currentUnitBaseStat.hp * 1f) / (_startUnitBaseStat.hp * 1f);
            }
        }
    }

    [Serializable]
    public struct UnitBaseStat
    {
        public new string name;
        public float hp;
        public float atk;
    }
}