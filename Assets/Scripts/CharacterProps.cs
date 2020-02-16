using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterProps : MonoBehaviour
{
    new public string name;
    public float health;
    public float mana;
    public float exp;
    public int level;
    public int coins;

    public int damage;
    public int armor;

    public string lines;

    void Start()
    {
        if (this.tag.Equals("Player"))
        {
            LoadSave();
        }
    }

    public void Save()
    {
        var playerPos = PlayerManager.instance.player.transform.GetChild(0).position;
        float[] position = new float[3];
        position[0] = playerPos.x;
        position[1] = playerPos.y;
        position[2] = playerPos.z;

        var list = PlayerManager.instance.inventory.items;
        Dictionary<int, int> inventoryItems = GetItemsForSave(list.ToArray());
        List<int> equipment = GetEquipmentID(EquipmentController.instance.currentEquipment);


        PlayerData playerData = new PlayerData(exp, level, coins, inventoryItems,  equipment, position, damage, armor);
        SaveSystem.SavePlayerProgress(playerData);
    }

    private Dictionary<int, int> GetItemsForSave(ItemAmount[] items)
    {
        Dictionary<int, int> itemsForSave = new Dictionary<int, int>();

        if (items.Length == 0) { return itemsForSave; }

        foreach(ItemAmount itemAmount in items)
        {
            if(itemAmount.item == null) { continue; }

            itemsForSave.Add(itemAmount.item.id, itemAmount.amount);
        }

        return itemsForSave;
    }

    public List<int> GetEquipmentID(Equipment[] equipment)
    {
        List<int> itemsID = new List<int>();

        if(equipment.Length == 0) { return itemsID; }

        foreach(Equipment item in equipment)
        {
            if(item == null) { continue; }
            itemsID.Add(item.id);
        }

        return itemsID;
    }

    public void LoadSave()
    {
        PlayerData playerData = SaveSystem.LoadPlayerProgress();

        if(playerData == null) { return; }

        LoadPlayer(playerData);
    }

    List<Item> FindItems(List<int> items)
    {
        Item[] savedItems = Resources.LoadAll<Item>("Items");

        List<Item> result = new List<Item>();

        foreach (Item item in savedItems)
        {
            if (items.Contains(item.id))
            {
                result.Add(item);
            }
        }

        return result;
    }

    private List<int> MultiplyItems(Dictionary<int, int> items)
    {
        List<int> result = new List<int>();

        foreach(KeyValuePair<int, int> pair in items)
        {
            for(int i = 0; i < pair.Value; i++)
            {
                result.Add(pair.Key);
            }
        }

        return result;
    }

    public void LoadPlayer(PlayerData playerData)
    {
        exp = playerData.exp;
        level = playerData.level;
        coins = playerData.coins;

        this.damage = playerData.damage;
        this.armor = playerData.armor;

        var savedItems = FindItems(MultiplyItems(playerData.items));
        PlayerManager.instance.inventory.AddListOfItems(savedItems);
            
        var savedEquipment = FindItems(playerData.equipment);
        EquipmentController.instance.EquipListOfItems(savedEquipment);

        GameObject player = PlayerManager.instance.player;

        player.transform.GetChild(0).position = new Vector3(playerData.position[0], playerData.position[1] + 1, playerData.position[2]);
    }
}
