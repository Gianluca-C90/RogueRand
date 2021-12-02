using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : Action
{
    public NavMeshAgent agent;
    public SharedGameObject enemy;
    public float speed;
    public SharedTransform target;
    public SharedBool inSight;
    public SharedBool atRange;

    Animator animator;


    public override void OnStart()
    {
        agent.speed = speed;
        float distance = Vector3.Distance(target.Value.position, transform.position);
        animator = enemy.Value.GetComponent<Animator>();
        animator.SetFloat("speed", agent.speed);
    }
    public override TaskStatus OnUpdate()
    {
        if (target != null)
        {
            Transform player = target.Value;
            transform.LookAt(player);
            if (inSight.Value == true)
            {
                transform.LookAt(player.transform);
                agent.SetDestination(player.position);
                return TaskStatus.Success;
            }
            else
                return TaskStatus.Failure;
        }
        else
            return TaskStatus.Failure;
    }

}
