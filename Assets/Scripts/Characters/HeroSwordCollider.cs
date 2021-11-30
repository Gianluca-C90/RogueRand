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
                //Raise evento che ha colpito character
                atkLanded.Raise();
                weaponCollider.enabled = false;
                Debug.Log("Enemy have been Hit!");
            }
        }
    }
}
