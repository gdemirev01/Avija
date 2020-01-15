using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = newItem.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem()
    {
        Debug.Log("click");
        if(item != null)
        {
            item.Use();
        }
    }
}
