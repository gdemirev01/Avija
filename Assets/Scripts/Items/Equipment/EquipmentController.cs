using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    #region Singleton
    public static EquipmentController instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    Inventory inventory;

    public delegate void OnEquipmentChanged();
    public OnEquipmentChanged onEquipmentChanged;

    public SkinnedMeshRenderer targetMesh;

    public Equipment[] currentEquipment;

    SkinnedMeshRenderer[] currentMeshes;

    private CharacterProps playerProps;

    private void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

        currentMeshes = new SkinnedMeshRenderer[numSlots];

        playerProps = PlayerManager.instance.player.GetComponent<CharacterProps>();

        onEquipmentChanged += UpdateStats;
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke();
        }
    }

    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            if(currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

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
        foreach (Equipment item in currentEquipment)
        {
            if (item == null) { continue; }

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
