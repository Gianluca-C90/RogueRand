using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Combat : Action
{
    public SharedBool atRange;
    public OffensiveSet moveset;
    public Animator animator;

    private ListOfAttacks tempAtk;

    public override void OnStart()
    {
        moveset.BuildWeightedListOfAttacks();
        StartCoroutine(Attack());
        animator.SetFloat("speed", 0);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Running;
    }

    IEnumerator Attack()
    {
        do
        {
            tempAtk = moveset.PickARandomAttack();
            float delay = tempAtk.atkRate;
            animator.SetTrigger("combo1");
            yield return new WaitForSeconds(delay);
        } while (atRange.Value);
    }
}
