using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayInventory : MonoBehaviour
{

    [SerializeField]
    InventoryObject inventory;

    private void OnEnable() 
    {
        List<InventorySlot> container = inventory.GetInventorySlots();

        for (int i = 0; i < container.Count; i++)
        {
            Image img = transform.GetChild(i).GetComponentInChildren<Image>();
            TMP_Text amount = transform.GetChild(i).GetComponentInChildren<TMP_Text>();
            
            img.sprite = container[i].item.image;
            amount.text = container[i].amount.ToString();    
        }
    }
}
