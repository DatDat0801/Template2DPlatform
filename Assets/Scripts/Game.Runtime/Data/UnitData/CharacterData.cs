using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    [CreateAssetMenu(fileName = "CharacterData",menuName = "DatDat/Data/CreateCharacterData")]
    public class CharacterData : UnitData
    {
        [Header("Player Data")]
        public TypeCharacter typeCharacter;
        public float jumpForce;
    }
}
