using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Slot[] slots;

    public Transform slotBoard;


    void Start()
    {
        GameManager.Instance.Player.addItem += AddItem;

        slots = new Slot[slotBoard.childCount];

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotBoard.GetChild(i).GetComponent<Slot>(); //GetChild ±î¸ÔÁö ¸»±â..
            slots[i].index = i;
            slots[i].inventory = this;
        }
    }

    void Update()
    {
        
    }

    void UpdateSlot()
    {
        for(int i = 0;i < slots.Length;i++)
        {
            if (slots[i].slotData != null)
            {
                slots[i].SetSlot();

            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    void AddItem()
    {
        ItemData data = GameManager.Instance.Player.itemData;

        if(data.stackable)
        {
            Slot filledSlot = GetFilledSlot(data);
            if(filledSlot != null )
            {
                filledSlot.quantity++;
                UpdateSlot();
                GameManager.Instance.Player.itemData = null;
                return;
            }
        }
        else
        {
            Slot emptySlot = GetEmptySlot();
            if(emptySlot != null)
            {
                emptySlot.slotData = data;
                emptySlot.quantity = 1;
                UpdateSlot();
                GameManager.Instance.Player.itemData = null;
                return;
            }

            Debug.Log("ÀÎº¥Åä¸®°¡ ²Ë Ã¡½À´Ï´Ù");

            GameManager.Instance.Player.itemData = null;
        }
    }

    Slot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].slotData == null)
                return slots[i];
        }
        return null;
    }

    Slot GetFilledSlot(ItemData data)
    {
        for(int i =0; i< slots.Length; i++)
        {
            if (slots[i].slotData == data)
                return slots[i];
        }
        return null;
    }
}
