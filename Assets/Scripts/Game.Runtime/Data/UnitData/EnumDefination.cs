using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public enum UnitStatus
    {
        Idle=0,
        RunLeft=1,
        RunRight=2,
        Jump=3,
        Death=4
    }

    public enum TypeCharacter
    {
        WoodCutter=0,
        GraveRobber=1
    }
    
    public enum TypeEnemy
    {
        Centipede=0,

    }
    

    public enum UnitTag
    {
        Player=0,
        Enemy=1
    }

    public enum UnitSkill
    {
        MeleeAttack=-1,
        WoodExplosion=0,
        SummonStormHead=1,
        LightningFlash=2,
        ChainLightning=3
    }
}
