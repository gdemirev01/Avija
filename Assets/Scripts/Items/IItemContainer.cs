public interface IItemContainer
{
    Item GetItem(int index);
    ItemAmount GetItemAmount(int index);
    int ItemsCount();
    int ItemCount(Item item);
    bool ContainsItem(Item item);
    void AddItem(Item item);
    void RemoveItem(Item item);
    bool IsFull();
}
