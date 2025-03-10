using UnityEngine;

public class Items : MonoBehaviour
{
    public ItemData itemData;


    public void SendToInventory()
    {
        GameManager.Instance.Player.itemData = itemData;
        GameManager.Instance.Player.addItem.Invoke();
        Destroy(gameObject);
    }
}
