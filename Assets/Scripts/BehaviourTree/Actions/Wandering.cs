using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : Action
{
    public NavMeshAgent agent;
    public SharedGameObject enemy;
    public float speed;
    public float radius;
    Animator animator;

    public override void OnStart()
    {
        agent.speed = speed;
        animator = enemy.Value.GetComponent<Animator>();
        animator.SetFloat("speed", agent.speed);
    }
    public override TaskStatus OnUpdate()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(RandomNavMeshLocation());

        return TaskStatus.Success;      
    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * radius;
        randomPosition += transform.position;
        if ( NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
