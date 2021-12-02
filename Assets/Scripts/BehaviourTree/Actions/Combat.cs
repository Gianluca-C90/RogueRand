using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Combat : Action
{
    public SharedBool atRange;
    public SharedTransform target;
    public OffensiveSet moveset;
    public Animator animator;
    public NavMeshAgent agent;
    public Collider weaponCollider;

    private ListOfAttacks tempAtk;

    public override void OnStart()
    {
        StopAllCoroutines();
        moveset.BuildWeightedListOfAttacks();
        StartCoroutine(Attack());
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Running;
    }

    IEnumerator Attack()
    {
        animator.SetFloat("speed", 0);
        do
        {
            
            tempAtk = moveset.PickARandomAttack();
            float delay = tempAtk.atkRate;
            weaponCollider.enabled = true;
            animator.SetTrigger("combo1");
            yield return new WaitForSeconds(delay);
        } while (atRange.Value);
    }
}
