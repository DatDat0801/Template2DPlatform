using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class ChainLightningAnim : MonoBehaviour
    {
        [SerializeField]private Animator animator;
        public Action onAttack;
        public Action onDeath;
        
        public void OnAttack()
        {
            this.onAttack?.Invoke();
        }

        public void Death()
        {
            this.onDeath?.Invoke();
        }
    }
}
