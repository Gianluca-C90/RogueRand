using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public GameEvent death;

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
        Animator anim = GetComponent<Animator>();
        
        anim.SetBool("isDead", true);

        Destroy(GetComponent<CharacterController>());
        Destroy(GetComponent<CharacterMovementController>());
        Rigidbody rigid = gameObject.AddComponent<Rigidbody>();
        rigid.isKinematic = true;
    }
}
