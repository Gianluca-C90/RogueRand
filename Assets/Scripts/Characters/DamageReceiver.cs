using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] Slider slider;
    public void DamagedReceived(float damageAmount) 
    {
        Debug.Log("Fired Damage Taken!");
        if (slider.value > 0f)
            slider.value -= damageAmount;
    }
}
