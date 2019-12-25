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

    private int space = 20;

    List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Inventory Full");
            return;
        }
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }
}
