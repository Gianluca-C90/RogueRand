using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WInPortalCollider : MonoBehaviour
{
    [SerializeField] GameEvent evento;
    private void OnTriggerEnter(Collider other)
    {
        evento.Raise();
    }
}
