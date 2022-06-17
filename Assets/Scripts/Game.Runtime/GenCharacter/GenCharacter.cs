using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class GenCharacter : MonoBehaviour
    {
        public UnitData characterData;
       
        [SerializeField] private UnitBaseActionMng _unitBaseActionMng;
        private void Start()
        {
            foreach (var stat in this.characterData.unitStatus)
            {
                CharAddComponent(stat);
            }

            InitPlayer();
        }

        void InitPlayer()
        {
            _unitBaseActionMng.Init(this.characterData);
        }

        void CharAddComponent(UnitStatus status)
        {
            switch (status)
            {
                case UnitStatus.Idle:
                    if (gameObject.GetComponent<Idle>() == null)
                        gameObject.AddComponent(typeof(Idle));
                    break;
                case UnitStatus.RunLeft:
                    if (gameObject.GetComponent<MoveLeft>() == null)
                        gameObject.AddComponent(typeof(MoveLeft));
                    break;
                case UnitStatus.RunRight:
                    if (gameObject.GetComponent<MoveRight>() == null)
                        gameObject.AddComponent(typeof(MoveRight));
                    break;
                case UnitStatus.Jump:
                    if (gameObject.GetComponent<Jump>() == null)
                        gameObject.AddComponent(typeof(Jump));
                    break;
            }
        }
    }
}