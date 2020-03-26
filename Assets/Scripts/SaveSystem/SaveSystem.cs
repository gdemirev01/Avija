using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    public static PlayerData playerData;

    private GameObject player;
    private CharacterProps playerProps;
    private PlayerManager playerManager;
    private QuestController questController;

    private void Start()
    {
        player = PlayerManager.Instance.player;
        playerManager = PlayerManager.Instance;
        questController = QuestController.Instance;
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

    private List<T> FindItems<T>(List<int> items)
        where T : Item
    {
        T[] savedItems = Resources.LoadAll<T>("Items");

        List<T> result = new List<T>();

        for (int i = 0; i < items.Count; i++)
        {
            foreach (T item in savedItems)
            {
                if (items[i] == item.id)
                {
                    result.Add(item);
                }
            }
        }
        return result;
    }

    private List<Quest> FindQuests(int[] quests)
    {
        Quest[] activeQuests = Resources.LoadAll<Quest>("Quests");

        List<Quest> result = new List<Quest>();

        for (int i = 0; i < quests.Length; i++)
        {
            foreach (Quest quest in activeQuests)
            {
                if (quests[i] == quest.id)
                {
                    result.Add(quest);
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

        var savedItems = FindItems<Item>(MultiplyItems(playerData.items));
        playerManager.inventory.AddListOfItems(savedItems);

        var savedEquipment = FindItems<Equipment>(playerData.equipment);
        EquipmentController.Instance.EquipListOfItems(savedEquipment);

        var activeQuests = FindQuests(playerData.quests);
        QuestController.Instance.AddQuests(activeQuests.ToArray());

        player.transform.GetChild(0).position = new Vector3(playerData.position[0], playerData.position[1] + 1, playerData.position[2]);
    }

    public void Save()
    {
        playerProps = player.GetComponent<CharacterProps>();

        var playerPos = playerManager.player.transform.GetChild(0).position;
        float[] position = new float[3];
        position[0] = playerPos.x;
        position[1] = playerPos.y;
        position[2] = playerPos.z;

        var list = playerManager.inventory.items;
        var inventoryItems = GetItemsForSave(list.ToArray());

        var currentEquipment = EquipmentController.Instance.currentEquipment.ToList();
        currentEquipment.RemoveAll(Item => Item == null);
        var equipment = currentEquipment.Select(e => e.id).ToList();

        var quests = questController.GetQuests().Select((q) => q.id).ToArray();

        playerData = new PlayerData(playerProps.exp, playerProps.level, playerProps.coins, inventoryItems, equipment, quests, position, playerProps.damage, playerProps.armor);
        BinarySerializer.SavePlayerProgress(playerData);
    }

    public void LoadSave()
    {
        if (playerData == null)
        {
            questController.ResetAllQuests();
            return;
        }

        SetPlayerProps(playerData);
    }
}
