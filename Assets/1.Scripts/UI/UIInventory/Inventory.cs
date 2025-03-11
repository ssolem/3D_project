using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Slot[] slots;

    public Transform slotBoard;
    public ItemData selectedSlotItem;
    public int selectedSlotItemIndex = -1;

    [Header("Action")]
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public Button leaveButton;
    public Button useButton;
    public Button equipButton;
    public Button unequipButton;
    public Button dropButton;

    private bool inventoryState = false;
    private bool isEquipped;

    private PlayerCondition condition;
    public EquipItems equipItems;

    void Start()
    {
        condition = GameManager.Instance.Player.condition;

        GameManager.Instance.Player.addItem += AddItem;

        slots = new Slot[slotBoard.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotBoard.GetChild(i).GetComponent<Slot>(); //GetChild ±î¸ÔÁö ¸»±â..
            slots[i].index = i;
            slots[i].inventory = this;
        }

        this.gameObject.SetActive(false);
        ClearInventory();
    }

    void Update()
    {

    }

    void UpdateSlot()
    {
        for (int i = 0; i < slots.Length; i++)
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

        if (data.stackable)
        {
            Slot filledSlot = GetFilledSlot(data);
            if (filledSlot != null)
            {
                filledSlot.quantity++;
                UpdateSlot();
                GameManager.Instance.Player.itemData = null;
                return;
            }
        }
        Slot emptySlot = GetEmptySlot();
        if (emptySlot != null)
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
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].slotData == data)
            {
                if (slots[i].quantity < data.maxStack || data.itemType == ItemType.Equip)
                {
                    return slots[i];
                }
            }
        }
        return null;
    }
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            SetInventory();
        }
    }

    void SetInventory()
    {
        if (!inventoryState)
        {
            this.gameObject.SetActive(true);
            GameManager.Instance.Player.camControl.ChangeToInvenPer();
            GameManager.Instance.Player.controller.EnableMouseControl();
        }
        else
        {
            this.gameObject.SetActive(false);
            GameManager.Instance.Player.camControl.ChangeFromInvenPer();
            GameManager.Instance.Player.controller.DisableMouseControl();
            ClearInventory();
        }

        inventoryState = !inventoryState;
    }

    public void SetInventoryAction()
    {
        ClearInventory();
        itemName.text = selectedSlotItem.itemName;
        itemDescription.text = selectedSlotItem.itemDescription;
        if (selectedSlotItem.itemType == ItemType.Consume)
        {
            useButton.gameObject.SetActive(true);
        }
        else if (selectedSlotItem.itemType == ItemType.Equip)
        {
            if (!slots[selectedSlotItemIndex].equipped)
            {
                equipButton.gameObject.SetActive(true);
            }
            else
                unequipButton.gameObject.SetActive(true);
        }
    }

    public void ClearInventory()
    {
        itemName.text = string.Empty;
        itemDescription.text = string.Empty;
        useButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
        unequipButton.gameObject.SetActive(false);
    }

    public void OnUseButton()
    {
        for (int i = 0; i < selectedSlotItem.affectedStats.Length; i++)
        {
            switch (selectedSlotItem.affectedStats[i].affectType)
            {
                case StatsAffect.Health:
                    condition.HealHealth(selectedSlotItem.affectedStats[i].affectValue);
                    break;
                case StatsAffect.PassiveHealth:
                    condition.AddPassiveHealth(selectedSlotItem.affectedStats[i].affectValue);
                    break;
                case StatsAffect.Stamina:
                    condition.HealStamina(selectedSlotItem.affectedStats[i].affectValue);
                    break;
                case StatsAffect.PassiveStamina:
                    condition.AddPassiveStamina(selectedSlotItem.affectedStats[i].affectValue);
                    break;
            }
        }

        slots[selectedSlotItemIndex].quantity--;

        if (slots[selectedSlotItemIndex].quantity <= 0)
        {
            ResetSlot();
            SortSlots();
        }
    }

    public void ResetSlot()
    {
        slots[selectedSlotItemIndex].ClearSlot();
        selectedSlotItemIndex = -1;
        selectedSlotItem = null;
    }

    public void OnEquipButton()
    {
        if (slots[selectedSlotItemIndex].equipped)
        {
            OnUnEquipButton(selectedSlotItemIndex);
            return;
        }

        if (isEquipped)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].equipped)
                {
                    OnUnEquipButton(slots[i].index);
                    break;
                }
            }
        }

        for (int i = 0; i < selectedSlotItem.affectedStats.Length; i++)
        {
            switch (selectedSlotItem.affectedStats[i].affectType)
            {
                case StatsAffect.Health:
                    condition.HealHealth(selectedSlotItem.affectedStats[i].affectValue);
                    break;
                case StatsAffect.PassiveHealth:
                    condition.AddPassiveHealth(selectedSlotItem.affectedStats[i].affectValue);
                    break;
                case StatsAffect.Stamina:
                    condition.HealStamina(selectedSlotItem.affectedStats[i].affectValue);
                    break;
                case StatsAffect.PassiveStamina:
                    condition.AddPassiveStamina(selectedSlotItem.affectedStats[i].affectValue);
                    break;
            }
        }

        equipItems.Equip(selectedSlotItem);
        slots[selectedSlotItemIndex].equipped = true;
        isEquipped = true;
        SetInventoryAction();
    }

    public void OnUnEquipButton(int index)
    {
        for (int i = 0; i < slots[index].slotData.affectedStats.Length; i++)
        {
            switch (slots[index].slotData.affectedStats[i].affectType)
            {
                case StatsAffect.Health:
                    condition.LoseHealth(slots[index].slotData.affectedStats[i].affectValue);
                    break;
                case StatsAffect.PassiveHealth:
                    condition.SubtractPassiveHealth(slots[index].slotData.affectedStats[i].affectValue);
                    break;
                case StatsAffect.Stamina:
                    condition.LoseStamina(slots[index].slotData.affectedStats[i].affectValue);
                    break;
                case StatsAffect.PassiveStamina:
                    condition.SubtractPassiveStamina(slots[index].slotData.affectedStats[i].affectValue);
                    break;
            }
        }

        equipItems.Unequip();
        slots[index].equipped = false;
        isEquipped = false;
        SetInventoryAction();
    }

    public void OnLeaveButton()
    {
        SetInventory();
    }

    public void OnDropButton()
    {
        if (slots[selectedSlotItemIndex].equipped)
        {
            OnUnEquipButton(selectedSlotItemIndex);
        }

        Vector3 pos = GameManager.Instance.Player.transform.position + (GameManager.Instance.Player.transform.forward * 3) + new Vector3(0, 0.8f, 0);
        Instantiate(selectedSlotItem.itemPrefab, pos, Quaternion.identity);

        ResetSlot();
        SortSlots();
    }

    public void SortSlots()
    {
        for (int i = 0; i < slots.Length - 1; i++)
        {
            if (slots[i].slotData == null) 
            {
                for (int j = i + 1; j < slots.Length; j++)
                {
                    if (slots[j].slotData != null) 
                    {
                        slots[i].slotData = slots[j].slotData;
                        slots[i].quantity = slots[j].quantity;
                        slots[i].equipped = slots[j].equipped;
                        slots[j].ClearSlot();
                        break;
                    }
                }
            }
        }

        UpdateSlot();
    }
}
