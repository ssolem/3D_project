using System;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Equip,
    Consume
}

public enum StatsAffect
{
    Health,
    PassiveHealth, // playercondition hp.passivevalue
    Stamina,
    PassiveStamina //playercondition sp.passivevalue
}

[Serializable]
public class AffectedStats
{
    public StatsAffect affectType;
    public float affectValue;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Item Info")]
    public ItemType itemType;
    public string itemName;
    public string itemDescription;
    public Image itemImage;
    public GameObject itemPrefab;
    public bool stackable;
    public int maxStack;

    [Header("Affected Stats")]
    public AffectedStats[] affectedStats;

}
