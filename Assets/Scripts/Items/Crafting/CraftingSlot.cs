public class CraftingSlot : ItemSlot
{

    public override void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
