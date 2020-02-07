using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory : IItemContainer
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback = () => { }; 

    public int space;

    public List<ItemAmount> items;

    public void AddListOfItems(List<Item> items)
    {
        foreach(Item item in items)
        {
            this.AddItem(item);
        }
    }

    public int ItemCount(Item item)
    {
        return items.Select((i => i.item)).Count(i => i.Equals(item));
    }

    public bool ContainsItem(Item item)
    {
        return items.Select((i => i.item)).Contains(item) ? true : false;
    }

    public bool ContainsAllItems(ItemAmount[] itemsToCheck)
    {
        foreach (ItemAmount item in itemsToCheck)
        {
            if(ItemCount(item.item) != item.amount)
            {
                return false;
            }
        }
        return true;
    }

    public void AddItem(Item item)
    {
        var listOfItems = items.Select((i) => i.item).ToList();

        if (IsFull())
        {
            UIController.instance.SetAlertMessage("Inventory is full. Clear up some space");
            return;
        }

        if (listOfItems.Contains(item))
        {
            var resultItem = items.Find(i => i.item.Equals(item));
            resultItem.amount++;
        }
        else
        {
            var newItem = new ItemAmount(item, 1);
            items.Add(newItem);
        }

        onItemChangedCallback.Invoke();
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item.Equals(item))
            {
                var it = items[i];
                it.amount--;

                if (it.amount <= 0)
                {
                    items.RemoveAt(i);
                }
            }
        }
        onItemChangedCallback.Invoke();
    }

    public void RemoveAllItems(ItemAmount[] itemsToRemove)
    {
        foreach (ItemAmount item in itemsToRemove)
        {
            for (int i = 0; i < item.amount; i++)
            {
                RemoveItem(item.item);
            }
        }
    }

    public bool IsFull()
    {
        if(items.Count > this.space)
        {
            return true;
        }
        return false;
    }

    public Item GetItem(int index)
    {
        return items[index].item;
    }

    public ItemAmount GetItemAmount(int index)
    {
        return items[index];
    }

    public int ItemsCount()
    {
        return items.Count;
    }
}

