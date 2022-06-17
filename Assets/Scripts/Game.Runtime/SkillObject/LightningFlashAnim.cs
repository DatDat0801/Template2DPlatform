using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class LightningFlashAnim : MonoBehaviour
    {
        [SerializeField]private Animator animator;
        public Action onAttack;
        public Action onDeath;
        private static readonly int s_attack = Animator.StringToHash("attack");

        public void SetAttack()
        {
            this.animator.SetTrigger(s_attack);
        }

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
