using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    //public SkinnedMeshRenderer mesh;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        EquipmentController.instance.Equip(this);
        PlayerManager.instance.inventory.RemoveItem(this);
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Feet}