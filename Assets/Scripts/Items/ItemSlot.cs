using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI text;
    public TextMeshProUGUI amount;
    public ClickableObject button;

    public Item item;

    private void Start()
    {
        if (button != null)
        {
            button.onLeft.AddListener(UseItem);
        }
    }

    public virtual void AddItem(ItemAmount newItem)
    {
        item = newItem.item;

        text.text = newItem.item.name;
        amount.text = newItem.amount.ToString();

        this.icon.sprite = item.icon;
        this.icon.enabled = true;
    }

    public void ClearSlot()
    {
        if (item == null) 
        { 
            return;
        }

        item = null;

        amount.text = "";

        icon.sprite = null;
        icon.enabled = false;
    }

    public virtual void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
