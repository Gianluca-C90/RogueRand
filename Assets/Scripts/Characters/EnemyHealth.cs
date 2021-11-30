using BehaviorDesigner.Runtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public GameEvent death;
    public NavMeshAgent agent;
    public Animator anim;
    public BehaviorTree tree;

    public void HealthChanges(int amount)
    {
        health += amount;

        if (health <= 0)
        {
            death.Raise();
            Death();
            Debug.Log(gameObject.name + " is Dead!");
        }
    }

    void Death()
    {
        anim.SetBool("isDead", true);

        var isDeadBool = (SharedBool)tree.GetVariable("isDead");
        isDeadBool.Value = true;
        Rigidbody rigid = gameObject.GetComponent<Rigidbody>();
        rigid.isKinematic = true;
        Destroy(agent, 1);
        Destroy(gameObject, 10);
    }
}
