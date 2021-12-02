using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    COMMON,
    UNCOMMON,
    RARE,
    LEGENDARY,
    EPIC
}

[CreateAssetMenu(fileName = "New NFT Object", menuName = "Inventory System/Items/NFT")]

public class NFTObject : ItemObject
{
    public Rarity rarity;

    private void Awake()
    {
        type = ItemType.NFT;
    }
}
