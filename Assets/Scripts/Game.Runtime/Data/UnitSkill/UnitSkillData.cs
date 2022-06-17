using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Game.Runtime
{
    [CreateAssetMenu(fileName = "UnitSkill", menuName = "DatDat/Skill/Create Unit Skill")]
    public class UnitSkillData : ScriptableObject
    {
        public int idSkill;
        public string name;
        public UnitSkill unitSkill;
        public float value;// maybe dmg, percent add hp,....
        public float cooldown;
    }

}
