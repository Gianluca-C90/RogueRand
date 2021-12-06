using System;
using System.Collections.Generic;
using UnityEngine;

public class HarvestButton : MonoBehaviour
{
    public InventoryObject inventory;

    public void Harvest()
    {

        List<InventorySlot> slots = inventory.GetInventorySlots();

        List<ulong> ids = new List<ulong>();
        List<ulong> amounts = new List<ulong>();

        foreach (var slot in slots)
        {
            ids.Add(slot.item.id);
            amounts.Add(Convert.ToUInt64(slot.amount));
        }
#if !UNITY_EDITOR

        AlgoServer.instance.HarvestASA(ids, amounts);
#else
        Debug.Log("HarvestClicked");
#endif
    }
}
