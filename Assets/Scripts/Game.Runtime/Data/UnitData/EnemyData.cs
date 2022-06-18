using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    [CreateAssetMenu(fileName = "CharacterData",menuName = "DatDat/Data/CreateEnemyData")]
    public class EnemyData : UnitData
    {
        [Header("Enemy Data")]
        public TypeEnemy typeEnemy;
        public float distanceStopBeforePlayer;
        public float cooldownAttack;
    }
}
