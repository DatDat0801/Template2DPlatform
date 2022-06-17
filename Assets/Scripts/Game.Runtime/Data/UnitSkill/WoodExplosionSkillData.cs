using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    /// <summary>
    /// moi khi enemy chet se moc ra 1 cay o cho enemy
    /// dat du dieu kien so luong: countTreeTrigger. thi se phat no
    /// </summary>
    [CreateAssetMenu(fileName = "UnitSkill", menuName = "DatDat/Skill/Wood Explosion")]
    public class WoodExplosionSkillData : UnitSkillData
    {
        public int countTreeTrigger;
    }
}
