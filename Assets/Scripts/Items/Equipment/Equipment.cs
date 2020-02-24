using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        EquipmentController.Instance.Equip(this);
        PlayerManager.Instance.inventory.RemoveItem(this);
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Feet}