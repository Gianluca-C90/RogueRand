using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithinRange : Conditional
{
    public float attackRange;
    public SharedBool atRange;
    public SharedBool inSight;
    public SharedBool isDead;

    public string targetTag;
    private Transform player;
    private GameObject target;

    public override void OnAwake()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
        player = target.transform;
    }

    public override TaskStatus OnUpdate()
    {

        float distanceToTarget = Vector3.Distance(transform.position, player.position);

        if (distanceToTarget <= attackRange && inSight.Value && !isDead.Value)
        {
            transform.LookAt(player.position);
            atRange.Value = true;
            return TaskStatus.Success;
        }
        else
        {
            atRange.Value = false;
            return TaskStatus.Failure;
        }
    }
}
