using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Runtime
{
    public class UnitData : ScriptableObject
    {
        [Header("Unit Base")]
        public int id;
        public UnitBaseStat unitStat;
        public float runSpeed;
        public List<UnitStatus> unitStatus;
    }
}
