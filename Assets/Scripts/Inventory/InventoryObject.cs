using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Object", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> container = new List<InventorySlot>();

    public void AddItem(ItemObject item, int amount)
    {
        foreach (var slot in container)
        {
            if (slot.item == item)
            {
                slot.AddAmount(amount);
                return;
            }
        }

        container.Add(new InventorySlot(item, amount));
    }

    public List<ulong> GetIDs()
    {
        List<ulong> ids = new List<ulong>();

        foreach (var item in container)
        {
            ids.Add(item.item.id);
        }

        return ids;
    }

    public List<InventorySlot> GetInventorySlots()
    {
        List<InventorySlot> slots = new List<InventorySlot>();

        foreach (var slot in container)
        {
            slots.Add(slot);
        }

        return slots;
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    public InventorySlot(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
