using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] GameEvent atkLanded;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Raise evento che ha colpito character
            atkLanded.Raise();
            Debug.Log("sei stato colpito!");
        }
    }
}
