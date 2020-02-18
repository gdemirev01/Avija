using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    public static PlayerData playerData;

    private GameObject player;
    private CharacterProps playerProps;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        player = PlayerManager.Instance.player;
    }

    private Dictionary<int, int> GetItemsForSave(ItemAmount[] items)
    {
        Dictionary<int, int> itemsForSave = new Dictionary<int, int>();

        if (items.Length == 0) 
        { 
            return itemsForSave;
        }

        foreach (ItemAmount itemAmount in items)
        {
            if (itemAmount.item == null)
            {
                continue; 
            }

            itemsForSave.Add(itemAmount.item.id, itemAmount.amount);
        }

        return itemsForSave;
    }

    private List<int> GetEquipmentID(Equipment[] equipment)
    {
        List<int> itemsID = new List<int>();

        if (equipment.Length == 0) 
        { 
            return itemsID;
        }

        foreach (Equipment item in equipment)
        {
            if (item == null)
            { 
                continue; 
            }
            itemsID.Add(item.id);
        }

        return itemsID;
    }

    private List<Item> FindItems(List<int> items)
    {
        Item[] savedItems = Resources.LoadAll<Item>("Items");

        List<Item> result = new List<Item>();

        for (int i = 0; i < items.Count; i++)
        {
            foreach (Item item in savedItems)
            {
                if (items[i] == item.id)
                {
                    result.Add(item);
                }
            }
        }
        return result;
    }

    private List<int> MultiplyItems(Dictionary<int, int> items)
    {
        List<int> result = new List<int>();

        foreach (KeyValuePair<int, int> pair in items)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                result.Add(pair.Key);
            }
        }

        return result;
    }

    private void SetPlayerProps(PlayerData playerData)
    {
        playerProps = player.GetComponent<CharacterProps>();

        playerProps.exp = playerData.exp;
        playerProps.level = playerData.level;
        playerProps.coins = playerData.coins;

        playerProps.damage = playerData.damage;
        playerProps.armor = playerData.armor;

        var savedItems = FindItems(MultiplyItems(playerData.items));
        PlayerManager.Instance.inventory.AddListOfItems(savedItems);

        var savedEquipment = FindItems(playerData.equipment);
        EquipmentController.Instance.EquipListOfItems(savedEquipment);

        player.transform.GetChild(0).position = new Vector3(playerData.position[0], playerData.position[1] + 1, playerData.position[2]);
    }

    public void Save()
    {
        playerProps = player.GetComponent<CharacterProps>();

        var playerPos = PlayerManager.Instance.player.transform.GetChild(0).position;
        float[] position = new float[3];
        position[0] = playerPos.x;
        position[1] = playerPos.y;
        position[2] = playerPos.z;

        var list = PlayerManager.Instance.inventory.items;
        var inventoryItems = GetItemsForSave(list.ToArray());
        var equipment = GetEquipmentID(EquipmentController.Instance.currentEquipment);

        playerData = new PlayerData(playerProps.exp, playerProps.level, playerProps.coins, inventoryItems, equipment, position, playerProps.damage, playerProps.armor);
        BinarySerializer.SavePlayerProgress(playerData);
    }

    public void LoadSave()
    {
        if (playerData == null)
        {
            QuestController.Instance.ResetAllQuests();
            return;
        }

        SetPlayerProps(playerData);
    }
}
