using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    [CreateAssetMenu(fileName = "UnitSkill", menuName = "DatDat/Skill/Lightning Flash Skill")]
    public class LightningFlashSkillData : UnitSkillData
    {
        public int quantity; // so luong set
        public float spaceX; // khoang cach giua nhung object
        public bool bothSide;
    }
}
