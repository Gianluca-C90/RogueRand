using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryObject inventory;

    public GameEvent coinPick;
    public GameEvent NFTPick;

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            switch (item.item.type)
            {
                case ItemType.COIN:
                    coinPick.Raise();
                    break;
                case ItemType.NFT:
                    NFTPick.Raise();
                    break;
            }
            inventory.AddItem(item.item, 1);
            other.gameObject.SetActive(false);
        }
    }
}
