using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public float exp;
    public int level;
    public int coins;
    public Dictionary<int, int> items;
    public List<int> equipment;
    public float[] position;

    public int damage;
    public int armor;

    public PlayerData(float exp, int level, int coins, Dictionary<int, int> items, List<int> equipment, float[] position, int damage, int armor)
    {
        this.exp = exp;
        this.level = level;
        this.coins = coins;
        this.items = items;
        this.equipment = equipment;
        this.position = position;
        this.damage = damage;
        this.armor = armor;
    }
}
