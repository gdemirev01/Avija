using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        this.icon.sprite = newItem.icon;
        this.icon.enabled = true;
    }

    public void ClearSlot()
    {
        if(item == null) { return; }
        
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
