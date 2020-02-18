using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int id;
    new public string name;
    public int cost;

    public static int itemsCount = 0;

    public Sprite icon;

    private void Awake()
    {
        itemsCount++;
        this.id = itemsCount;
    }

    public virtual void Use() { }
}

