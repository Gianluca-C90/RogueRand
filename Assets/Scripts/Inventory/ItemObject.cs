using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    FOOD,
    NFT,
    COIN
}

public class ItemObject : ScriptableObject
{
    public ItemType type;
    public GameObject prefab;
    public ulong id;
}
