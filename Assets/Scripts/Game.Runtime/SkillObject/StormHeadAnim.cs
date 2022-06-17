using System;
using UnityEngine;

namespace Game.Runtime
{
    public class StormHeadAnim : MonoBehaviour
    {
        [SerializeField]private Animator animator;
        public Action onAttackLeft;
        public Action onAttackRight;
        public Action onDeath;
        private static readonly int s_attack = Animator.StringToHash("attack");
        private static readonly int s_death = Animator.StringToHash("death");

        public void SetAttack()
        {
            this.animator.SetTrigger(s_attack);
        }

        public void OnAttackLeft()
        {
            this.onAttackLeft?.Invoke();
        }
        
        public void OnAttackRight()
        {
            this.onAttackRight?.Invoke();
        }
        
        public void Death()
        {
            this.onDeath?.Invoke();
        }
    }
}
