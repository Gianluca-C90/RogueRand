using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] GameEvent atkLanded;
    [SerializeField] Collider weaponCollider;

    void OnTriggerEnter(Collider collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //Raise evento che ha colpito character
                atkLanded.Raise();
                weaponCollider.enabled = false;
                //Debug.Log("You've been Hit!");
            }
            else if (collision.gameObject.CompareTag("ShieldedPlayer"))
            {
                //Debug.Log("Attack Blocked!");
            }
        }
    }
}
