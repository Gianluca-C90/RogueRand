using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System;
using UnityEngine.Events;

public class Dead : Conditional
{
    public SharedBool isDead;

    public override void OnAwake()
    {
        base.OnAwake();
    }

    public override TaskStatus OnUpdate()
    {
        if (isDead.Value)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }

    public void Death()
    {
        isDead = true;
    }
}
