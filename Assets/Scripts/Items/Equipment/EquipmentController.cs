using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : Singleton<EquipmentController>
{
    Inventory inventory;

    public delegate void OnEquipmentChanged();
    public OnEquipmentChanged onEquipmentChanged;

    public Equipment[] currentEquipment;

    private CharacterProps playerProps;

    public override void Awake()
    {
        base.Awake();

        currentEquipment = new Equipment[5];
    }

    private void Start()
    {
        inventory = PlayerManager.Instance.inventory;

        playerProps = PlayerManager.Instance.player.GetComponent<CharacterProps>();

        onEquipmentChanged += UpdateStats;
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);
        }

        currentEquipment[slotIndex] = newItem;

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke();
        }
    }

    public void EquipListOfItems(List<Item> items)
    {
        foreach(Equipment item in items)
        {
            this.Equip(item);
        }
    }

    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke();
            }
        }
    }

    public void UnequipAll()
    {
        for(int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void UpdateStats()
    {
        playerProps.armor = 0;
        playerProps.damage = 0;

        foreach (Equipment item in currentEquipment)
        {
            if (item == null)
            { 
                continue;
            }

            playerProps.armor += item.armorModifier;
            playerProps.damage += item.damageModifier;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
