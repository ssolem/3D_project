using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public enum ItemType
{
    Equip,
    Consume
}

public enum StatsAffect
{
    Health,
    Stamina
}

public class AffectedStats
{
    public StatsAffect affectType;
    public float affectValue;
}
public class ItemData : ScriptableObject
{
    [Header("Item Info")]
    public ItemType itemType;
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public bool stackable;
    public int itemCount;

    [Header("Affected Stats")]
    public AffectedStats[] affectedStats;
}
