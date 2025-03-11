using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItems : MonoBehaviour
{
    public Items equipItem;

    public void Equip(ItemData data)
    {
        equipItem = Instantiate(data.EquipPrefab, this.transform).GetComponent<Items>();
    }

    public void Unequip()
    {
        Destroy(equipItem.gameObject);
        equipItem = null;
    }
}
