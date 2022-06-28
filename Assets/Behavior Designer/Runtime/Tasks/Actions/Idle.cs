

using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Returns a TaskStatus of running. Will only stop when interrupted or a conditional abort is triggered.")]
    [TaskIcon("{SkinColor}IdleIcon.png")]
    public class Idle : Action
    {
        public override TaskStatus OnUpdate()
        {
            Debug.Log("idle");
            return TaskStatus.Running;
        }
    }
}