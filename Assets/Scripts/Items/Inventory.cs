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
    public OnItemChanged onItemChangedCallback; 

    public int space = 20;

    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Inventory Full");
            return;
        }
        items.Add(item);
        onItemChangedCallback.Invoke();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        onItemChangedCallback.Invoke();
    }
}
