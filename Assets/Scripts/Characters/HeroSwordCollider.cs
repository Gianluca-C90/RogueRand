using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSwordCollider : MonoBehaviour
{
    [SerializeField] GameEvent atkLanded;
    [SerializeField] Collider weaponCollider;

    void OnTriggerEnter(Collider collision)
    {
        if (collision != null)
        {

            if (collision.gameObject.CompareTag("Enemy"))
            {
                weaponCollider.enabled = false;
                collision.GetComponent<EnemyHealth>().HealthChanges(-1);
                //Debug.Log("Enemy have been Hit!");
            }
        }
    }
}
