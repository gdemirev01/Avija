using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There is another instance of inventory");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback = new OnItemChanged(() => { }); 

    public int space = 9;

    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Inventory Full");
            return;
        }

        if(items.Contains(item))
        {
            items.Find(i => i.Equals(item)).quantity++;
        }
        else
        {
            items.Add(item);
        }

        onItemChangedCallback.Invoke();
    }

    public void AddListOfItems(List<Item> items)
    {
        foreach(Item item in items)
        {
            this.Add(item);
        }
    }

    public void Remove(Item item)
    {
        item.quantity--;
        if (item.quantity == 0)
        {
            items.Remove(item);
            onItemChangedCallback.Invoke();
        }
    }
}
