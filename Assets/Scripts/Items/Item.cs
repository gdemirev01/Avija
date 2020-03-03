using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int id;
    
    new public string name;
    
    public int cost;

    public Sprite icon;

    public virtual void Use() { }
}

