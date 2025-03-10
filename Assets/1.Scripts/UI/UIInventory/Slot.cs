using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemData slotData;

    public Image slotImage;
    public int index;
    public int quantity;
    public TextMeshProUGUI itemQuantity;

    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;
    public Inventory inventory;
    private Outline outline;
    public Button slotButton;
    public Color green;
    public Color red;

    public bool equipped;

    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        outline = GetComponent<Outline>();
    }

    void Update()
    {
        if (slotData != null)
        {
            if (PointedSlot())
            {
                this.outline.enabled = true;
                this.outline.effectColor = green;
            }
            else
            {
                this.outline.enabled = false;
            }
        }
        if(equipped)
        {
            SetOutlineRed();
        }
    }

    public void SetSlot()
    {
        slotImage.gameObject.SetActive(true);
        slotButton.gameObject.SetActive(true);
        slotImage.sprite = slotData.itemImage;
        itemQuantity.text = quantity.ToString();
    }

    public void ClearSlot()
    {
        slotImage.sprite = null;
        itemQuantity.text = string.Empty;
        slotImage.gameObject.SetActive(false);
        slotButton.gameObject.SetActive(false);
        slotData = null;
    }

    bool PointedSlot()
    {
        PointerEventData data = new PointerEventData(eventSystem);
        data.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(data, results);

        return results.Count > 0;
    }

    public void OnClickSlot()
    {
        inventory.selectedSlotItem = slotData;
        inventory.selectedSlotItemIndex = index;
        inventory.SetInventoryAction();
    }

    public void SetOutlineRed()
    {
        this.outline.enabled = true;
        this.outline.effectColor = red;
    }

    public void ClearOutline()
    {
        this.outline.enabled = false;
    }
}
