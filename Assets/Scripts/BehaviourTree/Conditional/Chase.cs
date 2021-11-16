using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Action
{
    public CharacterController characterController;
    public float speed;
    public SharedTransform target;
    public SharedBool inSight;

    public override TaskStatus OnUpdate()
    {
        if (target != null)
        {
            Transform player = target.Value;
            Vector3 destination = player.position - transform.position;
            transform.LookAt(player);
            if (inSight.Value == true)
            {
                do
                {
                    characterController.Move(transform.forward * (speed * Time.deltaTime));
                    return TaskStatus.Success;
                } while (Vector3.Distance(player.position, transform.position) > 1);
            }
            else
                return TaskStatus.Failure;
        }
        else
        {
            return TaskStatus.Failure;
        }
            
    }
}
