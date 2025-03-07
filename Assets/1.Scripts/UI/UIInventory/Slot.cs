using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemData slotData;

    public Image slotImage;
    public int index;
    public int quantity;
    public TextMeshProUGUI itemQuantity;

    public Inventory inventory;

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetSlot()
    {
        slotImage.gameObject.SetActive(true) ;
        slotImage = slotData.itemImage;
        itemQuantity.text = quantity.ToString();
    }

    public void ClearSlot()
    {
        slotImage = null ;
        itemQuantity.text = string.Empty;
        slotImage.gameObject.SetActive(false);
        slotData = null;
    }
}
